using Newtonsoft.Json;
using System;

namespace TsSolutions.Serialization
{
    public class StoreableJsonValue1x0
    {
        [JsonProperty]
        public String Data { get; set; }

        [JsonProperty]
        public String Type { get; set; }
    }
}