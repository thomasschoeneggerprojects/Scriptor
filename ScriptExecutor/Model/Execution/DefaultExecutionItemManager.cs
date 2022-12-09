using ScriptExecutorLib.Model.Execution.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolutions.Service;
using TsSolutions.Storage;
using TsSolutions.Storage.FileStorage;

namespace ScriptExecutorLib.Model.Execution
{
    internal class DefaultExecutionItemManager : SimpleThreadServiceAsync, IExecutionItemManager
    {
        private ExecutionItemRepository _repository;

        public int MaxCapacity => 300;

        public DefaultExecutionItemManager()
        {
            _repository = new ExecutionItemRepository();
            _repository.StorageChanged += OnStorageChanged;
        }

        #region event handling

        public event EventHandler<ExecutionItemId> ItemAdded;

        public event EventHandler<ExecutionItemId> ItemUpdated;

        public event EventHandler<ExecutionItemId> ItemDeleted;

        public event EventHandler<ExecutionItemId> ItemChanged;

        private void OnStorageChanged(object? sender, StorageChangedInfo storageChangedInfo)
        {
            ExecutionItemId id = new ExecutionItemId(storageChangedInfo.ItemId);

            switch (storageChangedInfo.ChangeAction)
            {
                case StorageChangeAction.Unknown:
                    ItemChanged?.Invoke(this, id);
                    break;

                case StorageChangeAction.Added:
                    ItemAdded?.Invoke(this, id);
                    break;

                case StorageChangeAction.Updated:
                    ItemUpdated?.Invoke(this, id);
                    break;

                case StorageChangeAction.Deleted:
                    ItemDeleted?.Invoke(this, id);
                    break;

                default:
                    break;
            }
        }

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

        public async Task Add(ExecutionItem item)
        {
            if (_repository.AllItems.Count >= MaxCapacity)
                throw new ArgumentException($"Not allowed to {nameof(Add)} item." +
                    $"Max Value {nameof(MaxCapacity)} = {MaxCapacity} reached.");

            // TODO !!! CONSIDER Event in Lock Mechanism
            //await LockAsync(async () =>
            //{
            string filePath = _repository.CreateFilePath(item);

            await _repository.Add(item, filePath);
            //});
        }

        public async Task Update(ExecutionItem item)
        {
            // TODO !!! CONSIDER Event in Lock Mechanism
            //await LockAsync(async () =>
            //{
            string filePath = _repository.CreateFilePath(item);
            if (_repository.AllItems.Any(tmp => tmp.Id.Equals(item.Id)))
            {
                await _repository.Update(item, filePath);

                return;
            }

            throw new ArgumentException($"Item with id = {item.Id} does not exist in store. " +
                $"Please add it with {nameof(Add)} before.");
            //});
        }

        public async Task Delete(ExecutionItem item)
        {
            // TODO !!! CONSIDER Event in Lock Mechanism
            //await LockAsync(async () =>
            //{
            await _repository.Delete(item);
            //});
            //OnItemsChanged();
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

        public async Task Init()
        {
            await _repository.Init().ConfigureAwait(false);
        }
    }
}