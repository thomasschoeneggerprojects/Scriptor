using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolutions.Serialization.PropertySet;

namespace ScriptExecutorLib.Model.Execution
{
    public class ExecutionItem
    {
        public string Name { get; internal set; }

        public string Description { get; internal set; }

        public ExecutionItemId Id { get; internal set; }

        public string ItemFilePath { get; internal set; }

        public string Content { get; internal set; }

        public ExecutionItemType ItemType { get; internal set; }

        public DateTimeOffset LastModifiedDate { get; internal set; }

        public List<ExecutionItemArgument> Arguments { get; internal set; }

        public IPropertySet Properties
        {
            get; set;
        }
    }
}