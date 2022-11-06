using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolutions.Serialization.PropertySet;

namespace ScriptExecutorLib.Model.Execution
{
    internal class ExecutionItemFactory
    {
        internal static ExecutionItem CreateExecutionItemDefault(string name)
        {
            ExecutionItem newDefaultItem = new ExecutionItem();
            newDefaultItem.Id = new ExecutionItemId(Guid.NewGuid());
            newDefaultItem.Name = name;
            newDefaultItem.ItemFilePath = "\\";
            newDefaultItem.Content = "Add Content here";
            newDefaultItem.Description = "Any Description";
            newDefaultItem.Arguments = new List<ExecutionItemArgument>();
            newDefaultItem.LastModifiedDate = DateTimeOffset.Now;
            newDefaultItem.Properties = DefaultPropertySet.Empty();

            return newDefaultItem;
        }
    }
}