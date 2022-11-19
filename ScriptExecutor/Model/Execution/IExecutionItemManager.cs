using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Execution
{
    internal interface IExecutionItemManager
    {
        Task<List<ExecutionItemOverview>> GetAllOverviewItems();

        Task<List<ExecutionItem>> GetAll();

        Task<ExecutionItem> GetById(ExecutionItemId id);

        bool Exists(ExecutionItemId id);

        Task Update(ExecutionItem item);

        Task Delete(ExecutionItem item);

        Task DeleteAll();

        Task Init();
    }
}