using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Storage.FileStorage
{
    [System.Serializable]
    public class ItemUpdateInfoDto
    {
        #region Modified date

        public DateTimeOffset LastModifiedDate { get; set; } =
            DateTimeOffset.UtcNow;

        #endregion Modified date

        public Dictionary<Guid, DateTimeOffset> UpdatedItems { get; set; } =
            new Dictionary<Guid, DateTimeOffset>();

        public Guid RunningInstanceId { get; set; } =
            Guid.NewGuid();

        public bool IsUpdatedFromThisInstance { get; internal set; } = false;
    }
}