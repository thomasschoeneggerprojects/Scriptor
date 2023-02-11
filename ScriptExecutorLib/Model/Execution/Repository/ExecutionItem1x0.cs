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
    internal class ExecutionItem1x0
    {
        [JsonProperty]
        public Guid Guid
        {
            get; set;
        }

        [JsonProperty]
        public string Version
        {
            get; set;
        }

        [JsonProperty]
        public string Name
        {
            get; set;
        }

        [JsonProperty]
        public string Description
        {
            get; set;
        }

        [JsonProperty]
        public string Value
        {
            get; set;
        }

        [JsonProperty]
        public string LastModifiedDateString { get; set; }

        [JsonProperty]
        internal List<ExecutionItemArgument1x0> Arguments
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