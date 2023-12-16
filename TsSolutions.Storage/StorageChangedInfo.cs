using System;

namespace TsSolutions.Storage
{
    public class StorageChangedInfo
    {
        public DateTimeOffset ChangedDate { get; private set; }
        public StorageChangeAction ChangeAction { get; }

        public Guid ItemId { get; private set; }

        private StorageChangedInfo(StorageChangeAction changeAction, Guid itemId)
        {
            ChangedDate = DateTimeOffset.Now;
            ItemId = itemId;
            ChangeAction = changeAction;
        }

        internal static StorageChangedInfo Create(StorageChangeAction changeAction, Guid itemId)
        {
            return new StorageChangedInfo(changeAction, itemId);
        }
    }
}