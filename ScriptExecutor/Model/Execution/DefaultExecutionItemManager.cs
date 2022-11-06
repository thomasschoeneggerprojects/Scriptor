using ScriptExecutorLib.Model.Execution.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolutions.Service;
using TsSolutions.Storage.FileStorage;

namespace ScriptExecutorLib.Model.Execution
{
    internal class DefaultExecutionItemManager : SimpleThreadServiceAsync, IExecutionItemManager
    {
        private ExecutionItemRepository _repository;

        public DefaultExecutionItemManager()
        {
            _repository = new ExecutionItemRepository();
            _repository.StorageChanged += OnStorageChanged;
        }

        #region event handling

        public event EventHandler ItemsChanged;

        private void OnItemsChanged()
        {
            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void OnStorageChanged(object? sender, EventArgs e)
        {
            OnItemsChanged();
        }

        //private Task<bool> OnStorageChanged()
        //{
        //    OnItemsChanged();

        //    return Task.FromResult(true);
        //}

        #endregion event handling

        public Task<List<ExecutionItemOverview>> GetAllOverviewItems()
        {
            List<ExecutionItemOverview> mappedOverviewItems = Map(_repository.OverviewItems);

            return Task.FromResult(mappedOverviewItems);
        }

        private List<ExecutionItemOverview> Map(List<ItemOverviewInfo> overviewItems)
        {
            List<ExecutionItemOverview> mappedOverviewItems = new List<ExecutionItemOverview>();
            foreach (var overviewInfo in overviewItems)
            {
                ExecutionItemOverview eio = new ExecutionItemOverview();
                eio.Name = overviewInfo.DisplayName;
                eio.Description = overviewInfo.Description;
                eio.LastModifiedDate = overviewInfo.LastModifiedDate;
                eio.Id = new ExecutionItemId(overviewInfo.ItemGuid);
                mappedOverviewItems.Add(eio);
            }
            throw new NotImplementedException();
        }

        public async Task<List<ExecutionItem>> GetAll()
        {
            return _repository.AllItems;
        }

        public Task<ExecutionItem> GetById(ExecutionItemId id)
        {
            return Task.FromResult(_repository.AllItems.First(cont => cont.Id.Equals(id)));
        }

        public bool Exists(ExecutionItemId id)
        {
            return _repository.AllItems.Any(tmp => tmp.Id.Equals(id));
        }

        public async Task Update(ExecutionItem item)
        {
            // TODO !!! CONSIDER Event in Lock Mechanism
            await LockAsync(async () =>
            {
                string filePath = _repository.CreateFilePath(item);
                if (_repository.AllItems.Any(tmp => tmp.Id.Equals(item.Id)))
                {
                    await _repository.Save(item, filePath);

                    return;
                }

                await _repository.Save(item, filePath);
            });
        }

        public async Task Delete(ExecutionItem item)
        {
            // TODO !!! CONSIDER Event in Lock Mechanism
            //await LockAsync(async () =>
            //{
            await _repository.Delete(item);
            //});
        }

        public async Task DeleteAll()
        {
            // TODO !!! CONSIDER Event in Lock Mechanism
            //await LockAsync(async () =>
            //{
            await _repository.DeleteAll();
            //});
        }

        protected override void OnErrorOccured(ServiceErrorInformation errorInformation)
        {
            // Implement logging
        }
    }
}