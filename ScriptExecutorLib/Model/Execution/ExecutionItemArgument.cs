using ScriptExecutor.ViewModel;
using System;

namespace ScriptExecutorLib.Model.Execution
{
    [Serializable]
    public class ExecutionItemArgument
    {
        public ExecutionItemArgumentId Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public string ValuePrefix
        {
            get; set;
        }

        public string Value
        {
            get; set;
        }

        public string DefaultValue
        {
            get; set;
        }

        public string ValuePostfix
        {
            get; set;
        }
    }
}