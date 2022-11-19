using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Execution
{
    internal interface IExecutionItemProcessor
    {
        Task<ExecutionInformation> Run(ExecutionItem executionItem);
    }
}