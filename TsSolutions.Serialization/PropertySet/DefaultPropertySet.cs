using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Serialization.PropertySet
{
    public class DefaultPropertySet : IPropertySet
    {
        private Dictionary<Guid, object> _properties; 

        public DefaultPropertySet()
        {
            _properties = new Dictionary<Guid, object>();
        }

        public void Add<T>(PropertyKey key, T value)
        {
            if (key == null)
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }
            if (value == null)
            {
                throw new ArgumentException($"{nameof(value)} cannot be null");
            }

            _properties.Add(key.Key, value);
        }

        public bool Contains<T>(PropertyKey key)
        {
            if (key == null)
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }
            return _properties.ContainsKey(key.Key);
        }

        public T Get<T>(PropertyKey key)
        {
            if (key == null)
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }

            return (T)_properties[key.Key];
        }

        Dictionary<Guid, object> IPropertySet.GetItems()
        {
            return _properties;
        }

        public void Set<T>(PropertyKey key, T value)
        {
            if (key == null)
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }
            if (value == null)
            {
                throw new ArgumentException($"{nameof(value)} cannot be null");
            }

            _properties[key.Key] = value;
        }

        public bool TryGet<T>(PropertyKey key, out T value)
        {
            if (key == null)
            {
                throw new ArgumentException($"{nameof(key)} cannot be null");
            }

            if (_properties.ContainsKey(key.Key))
            {
                value = (T)_properties[key.Key];
                return true;
            }
            value = default(T);
            return false;
        }

        public static DefaultPropertySet Empty()
        {
            return new DefaultPropertySet();
        }
    }
}