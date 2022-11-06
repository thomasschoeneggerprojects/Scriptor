using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TsSolutions.Storage
{
    internal static class JsonStorageHandler
    {
        private static readonly object _lock = new object();

        public static async Task StoreAsync<T>(T jsonObject, string filepath)
        {
            lock (_lock)
            {
                var json = JsonConvert.SerializeObject(jsonObject);
                File.WriteAllText(filepath, json);
            }
        }

        public static async Task<T> LoadAsync<T>(string filepath)
        {
            lock (_lock)
            {
                string fileContent = File.ReadAllText(filepath);
                T data = JsonConvert.DeserializeObject<T>(fileContent);
                return data;
            }
        }

        public static void Store<T>(T jsonObject, string filepath)
        {
            lock (_lock)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(jsonObject);
                    File.WriteAllText(filepath, json, Encoding.UTF8);
                }
                catch (System.Exception ex)
                {
                    var test = ex;
                }
            }
        }

        public static T Load<T>(string filepath)
        {
            lock (_lock)
            {
                try
                {
                    string fileContent = File.ReadAllText(filepath);
                    T data = JsonConvert.DeserializeObject<T>(fileContent);
                    return data;
                }
                catch (System.Exception ex)
                {
                    var test = ex;
                }
                return default(T);
            }
        }
    }
}