using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Serialization.PropertySet
{
    public class PropertySetMapper
    {
        public static IPropertySet Map<D, E>(StoreableJsonDictionary1x0 storeableJsonDictionary1X0,
            params ValueConverter<D, E>[] valueConverters)
        {
            var propSet = new DefaultPropertySet();

            foreach (var item in storeableJsonDictionary1X0.Items)
            {
                var data = CreateDtoData(item.Value, item.Key, valueConverters);
                var propKey = PropertyKey.Create(item.Key, data.GetType());
                propSet.Add(propKey, data);
            }
            return propSet;
        }

        public static IPropertySet Map(StoreableJsonDictionary1x0 storeableJsonDictionary1X0)
        {
            var propSet = new DefaultPropertySet();

            foreach (var item in storeableJsonDictionary1X0.Items)
            {
                var data = CreateDtoData(item.Value, item.Key);
                var propKey = PropertyKey.Create(item.Key, data.GetType());
                propSet.Add(propKey, data);
            }
            return propSet;
        }

        public static StoreableJsonDictionary1x0 Map<D, E>(IPropertySet propertySet,
            params ValueConverter<D, E>[] valueConverters)
        {
            var storeableJsonDictionary1X0 = new StoreableJsonDictionary1x0();

            foreach (var item in propertySet.GetItems())
            {
                var value = new StoreableJsonValue1x0();
                value.Type = item.Value.GetType().FullName;
                value.Data = CreateJsonData(item.Value);
                storeableJsonDictionary1X0.Add(item.Key, value);
            }

            return storeableJsonDictionary1X0;
        }

        public static StoreableJsonDictionary1x0 Map(IPropertySet propertySet)
        {
            var storeableJsonDictionary1X0 = new StoreableJsonDictionary1x0();

            foreach (var item in propertySet.GetItems())
            {
                var value = new StoreableJsonValue1x0();
                value.Type = item.Value.GetType().FullName;
                value.Data = CreateJsonData(item.Value);
                storeableJsonDictionary1X0.Add(item.Key, value);
            }

            return storeableJsonDictionary1X0;
        }

        private static Object CreateDtoData<D, E>(StoreableJsonValue1x0 value, Guid id,
            params ValueConverter<D, E>[] valueConverters)
        {
            var type = Type.GetType(value.Type);

            if (type == typeof(bool) ||
                type == typeof(byte) ||
                type == typeof(int) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(long) ||
                type == typeof(string))
            {
                return Convert.ChangeType(value.Data, type);
            }

            foreach (var valueConverter in valueConverters)
            {
                if (valueConverter.DtoType.Equals(type))
                {
                    var data = JsonConvert.DeserializeObject<E>(value.Data);
                    var mappedObject = valueConverter.MapToDto(data);
                    return mappedObject;
                }
            }

            throw new ArgumentException($"{nameof(CreateJsonData)}: Data type not supported");
        }

        private static Object CreateDtoData(StoreableJsonValue1x0 value, Guid id)
        {
            var type = Type.GetType(value.Type);

            if (type == typeof(bool) ||
                type == typeof(byte) ||
                type == typeof(int) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(long) ||
                type == typeof(string))
            {
                return Convert.ChangeType(value.Data, type);
            }

            throw new ArgumentException($"{nameof(CreateJsonData)}: Data type not supported");
        }

        private static String CreateJsonData<D, E>(object dtoData, params ValueConverter<D, E>[] valueConverters)
        {
            var type = dtoData.GetType();

            if (type == typeof(bool) ||
                type == typeof(byte) ||
                type == typeof(int) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(long) ||
                type == typeof(string))
            {
                return dtoData.ToString();
            }

            foreach (var valueConverter in valueConverters)
            {
                if (valueConverter.EntityType.Equals(type))
                {
                    var mappedObject = valueConverter.MapToEntity((D)dtoData);
                    return JsonConvert.SerializeObject(mappedObject, valueConverter.EntityType, null);
                }
            }

            throw new ArgumentException($"{nameof(CreateJsonData)}: Data type not supported");
        }

        private static String CreateJsonData(object dtoData)
        {
            var type = dtoData.GetType();

            if (type == typeof(bool) ||
                type == typeof(byte) ||
                type == typeof(int) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(long) ||
                type == typeof(string))
            {
                return dtoData.ToString();
            }

            throw new ArgumentException($"{nameof(CreateJsonData)}: Data type not supported");
        }

        public static String CreateJsonData(object dtoData, StoreableJsonValue1x0 value)
        {
            var type = dtoData.GetType();

            if (type == typeof(bool) ||
                type == typeof(byte) ||
                type == typeof(int) ||
                type == typeof(float) ||
                type == typeof(double) ||
                type == typeof(long) ||
                type == typeof(string))
            {
                return dtoData.ToString();
            }

            throw new ArgumentException($"{nameof(CreateJsonData)}: Data type not supported");
        }
    }
}