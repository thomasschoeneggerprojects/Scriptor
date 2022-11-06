using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Serialization
{
    public class StoreableJsonDictionary1x0
    {
        [JsonProperty]
        private readonly Dictionary<Guid, StoreableJsonValue1x0> _values;

        public StoreableJsonDictionary1x0()
        {
            _values = new Dictionary<Guid, StoreableJsonValue1x0>();
        }

        internal Dictionary<Guid, StoreableJsonValue1x0> Items => _values;

        public StoreableJsonValue1x0 this[Guid key]
        {
            get
            {
                return _values[key];
            }
            set
            {
                _values[key] = value;
            }
        }

        public void Add(Guid key, StoreableJsonValue1x0 value)
        {
            _values.Add(key, value);
        }

        public bool ContainsKey(Guid key)
        {
            return _values.ContainsKey(key);
        }
    }
}