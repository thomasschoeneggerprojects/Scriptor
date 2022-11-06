using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Serialization.PropertySet
{
    public interface IPropertySet
    {
        void Add<T>(PropertyKey key, T value);

        bool Contains<T>(PropertyKey key);

        T Get<T>(PropertyKey key);

        Dictionary<Guid, object> GetItems();

        void Set<T>(PropertyKey key, T value);

        bool TryGet<T>(PropertyKey key, out T value);
    }
}