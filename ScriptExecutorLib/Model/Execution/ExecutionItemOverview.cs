using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Execution
{
    internal class ExecutionItemOverview
    {
        public ExecutionItemId Id { get; internal set; }

        public string Name { get; internal set; }

        public string Description { get; internal set; }

        public DateTimeOffset LastModifiedDate { get; internal set; }
    }
}