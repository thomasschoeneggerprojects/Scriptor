using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TsSolutions.Serialization;

namespace ScriptExecutorLib.Model.Execution.Repository
{
    [System.Serializable]
    internal class ExecutionItemArgument1x0
    {
        public Guid Guid
        {
            get; set;
        }

        public string Version
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

        [JsonProperty]
        internal StoreableJsonDictionary1x0 Properties
        {
            get; set;
        }
    }
}