using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Serialization.PropertySet
{
    public class PropertyKey
    {
        private PropertyKey(Guid key, Type value)
        {
            Key = key;
            ValueType = value;
        }

        public Guid Key { get; }

        public Type ValueType { get; }

        public static PropertyKey Create<T>(Guid key)
        {
            return new PropertyKey(key, typeof(T));
        }

        public static PropertyKey Create(Guid key, Type type)
        {
            return new PropertyKey(key, type);
        }
    }
}