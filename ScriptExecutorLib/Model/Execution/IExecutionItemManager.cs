using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Execution
{
    internal interface IExecutionItemManager
    {
        event EventHandler<ExecutionItemId> ItemAdded;

        event EventHandler<ExecutionItemId> ItemUpdated;

        event EventHandler<ExecutionItemId> ItemDeleted;

        event EventHandler<ExecutionItemId> ItemChanged;

        int MaxCapacity { get; }

        Task<List<ExecutionItemOverview>> GetAllOverviewItems();

        Task<List<ExecutionItem>> GetAll();

        Task<ExecutionItem> GetById(ExecutionItemId id);

        bool Exists(ExecutionItemId id);

        Task Add(ExecutionItem item);

        Task Update(ExecutionItem item);

        Task Delete(ExecutionItem item);

        Task DeleteAll();

        Task Init();
    }
}