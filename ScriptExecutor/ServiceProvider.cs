using ScriptExecutorLib.Model.Execution;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib
{
    internal static class ServiceProvider
    {
        private static bool isInitialized = false;
        private static Dictionary<string, object> services = new Dictionary<string, object> { };

        public static T Get<T>()
        {
            if (!isInitialized)
            {
                try
                {
                    InitializeDefaultServices();
                    isInitialized = true;
                }
                catch (Exception)
                {
                    throw;
                }
            }

            Type type = typeof(T);
            try
            {
                T service = (T)services[type.FullName];
                return service;
            }
            catch
            {
                throw new ArgumentException("The service does not exist. Please register first.");
            }
        }

        public static void Register<T>(object implementation)
        {
            Type type = typeof(T);

            if (services.ContainsKey(type.FullName))
            {
                services[type.FullName] = implementation;
                return;
            }
            services.Add(type.FullName, implementation);
        }

        public static void InitializeDefaultServices()
        {
            string registeredServices = String.Empty;
            try
            {
                Register<IExecutionItemManager>(new DefaultExecutionItemManager());
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Cannot Register service. Please check the correct order for registration. " +
                    $"Successful registered services:{registeredServices}\n exception {ex}");
            }
        }
    }
}