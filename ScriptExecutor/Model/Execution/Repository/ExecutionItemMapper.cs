using ScriptExecutor.ViewModel;
using System;
using System.Collections.Generic;
using TsSolutions.Serialization.PropertySet;
using TsSolutions.Storage.FileStorage;

namespace ScriptExecutorLib.Model.Execution.Repository
{
    internal class ExecutionItemMapper
    {
        internal static ExecutionItem1x0 Map(ExecutionItem dto)
        {
            ExecutionItem1x0 entity = new ExecutionItem1x0();
            entity.Version = "1.0.0";
            entity.Guid = dto.Id.Guid;
            entity.Name = dto.Name;
            entity.Description = dto.Description;
            entity.LastModifiedDateString = JsonMapper.Map(dto.LastModifiedDate);
            entity.Arguments = Map(dto.Arguments);
            entity.Properties = PropertySetMapper.Map(dto.Properties);

            return entity;
        }

        internal static ExecutionItem Map(ExecutionItem1x0 storageItem)

        {
            ExecutionItem entry = new ExecutionItem();
            entry.Id = new ExecutionItemId(storageItem.Guid);
            entry.Name = storageItem.Name;
            entry.Description = storageItem.Description;
            entry.LastModifiedDate = JsonMapper.MapToDateTimeOffset(storageItem.LastModifiedDateString);
            entry.Arguments = Map(storageItem.Arguments);
            entry.Properties = PropertySetMapper.Map(storageItem.Properties);

            return entry;
        }

        private static List<ExecutionItemArgument1x0> Map(List<ExecutionItemArgument> arguments)
        {
            List<ExecutionItemArgument1x0> mappedArguments = new List<ExecutionItemArgument1x0>();
            foreach (var argument in arguments)
            {
                mappedArguments.Add(Map(argument));
            }
            return mappedArguments;
        }

        private static List<ExecutionItemArgument> Map(List<ExecutionItemArgument1x0> arguments)
        {
            List<ExecutionItemArgument> mappedArguments = new List<ExecutionItemArgument>();
            foreach (var argument in arguments)
            {
                mappedArguments.Add(Map(argument));
            }
            return mappedArguments;
        }

        public static ExecutionItemArgument Map(ExecutionItemArgument1x0 item)
        {
            var itemToMap = new ExecutionItemArgument();

            itemToMap.Id = new ExecutionItemArgumentId(item.Guid);

            itemToMap.Name = item.Name;
            itemToMap.Description = item.Description;
            itemToMap.ValuePrefix = item.ValuePrefix;
            itemToMap.Value = item.Value;
            itemToMap.DefaultValue = item.DefaultValue;
            itemToMap.ValuePostfix = item.ValuePostfix;
            //itemToMap.Properties = PropertySetMapper.Map(item.Properties);

            return itemToMap;
        }

        public static ExecutionItemArgument1x0 Map(ExecutionItemArgument item)
        {
            var itemToMap = new ExecutionItemArgument1x0();

            itemToMap.Guid = item.Id.Guid;
            itemToMap.Name = item.Name;
            itemToMap.Description = item.Description;
            itemToMap.ValuePrefix = item.ValuePrefix;
            itemToMap.Value = item.Value;
            itemToMap.DefaultValue = item.DefaultValue;
            itemToMap.ValuePostfix = item.ValuePostfix;

            return itemToMap;
        }
    }
}