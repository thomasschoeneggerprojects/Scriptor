using ScriptExecutorLib.Model.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model
{
    public static class Startup
    {
        public static async Task Init()
        {
            var executionItemManager = ServiceProvider.Get<IExecutionItemManager>();
            await executionItemManager.Init();
        }
    }
}