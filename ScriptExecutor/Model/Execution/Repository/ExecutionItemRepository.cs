using ScriptExecutorLib.Model.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolutions.Storage;
using TsSolutions.Storage.FileStorage;

namespace ScriptExecutorLib.Model.Execution.Repository
{
    internal class ExecutionItemRepository : RepositoryBase<ExecutionItem1x0, ExecutionItem>
    {
        public ExecutionItemRepository()
        {
            VerifyFileType = (filename) => FileNameHelper.IsExecutionItemFile(filename);
        }

        public override void CreateOverviewItems(List<ExecutionItem> items)
        {
            OverviewItems = new List<ItemOverviewInfo>();
            foreach (var item in items)
            {
                OverviewItems.Add(
                new ItemOverviewInfo
                {
                    ItemGuid = item.Id.Guid,
                    DisplayName = item.Name,
                    Description = item.Description,
                    LastModifiedDate = item.LastModifiedDate
                });
            }
        }

        public override async Task Delete(ExecutionItem item)
        {
            await DeleteItem(item.Id.Guid, CreateFilePath(item));
        }

        public override async Task DeleteAll()
        {
            foreach (var folderPath in FolderPaths)
            {
                await DeleteAllItem(folderPath);
            }
        }

        public override Task<List<string>> GetFolderPaths()
        {
            return Task.FromResult(new List<string> { GlobalConstants.DefaultExecutionItemEntryFolderPath });
        }

        public override Task<ExecutionItem> Load(string filePath)
        {
            var item = LoadItem(filePath);
            return Task.FromResult(item);
        }

        public override ExecutionItem Map(ExecutionItem1x0 item)
        {
            return ExecutionItemMapper.Map(item);
        }

        public override ExecutionItem1x0 Map(ExecutionItem dto)
        {
            return ExecutionItemMapper.Map(dto);
        }

        public override async Task Save(ExecutionItem item, string filePath)
        {
            item.LastModifiedDate = DateTimeOffset.UtcNow;

            await SaveItem(item, item.Id.Guid, filePath);
        }

        public override string CreateFilePath(ExecutionItem item)
        {
            return Path.Combine(GlobalConstants.DefaultExecutionItemEntryFolderPath,
                FileNameHelper.GetFileName(item));
        }

        public override async Task Init()
        {
            await InitUpdaterFirst();
        }
    }
}