using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Storage.FileStorage
{
    [System.Serializable]
    public class ItemUpdateInfo1X0
    {
        #region Modified date

        public string LastModifiedDate
        {
            get; set;
        }

        #endregion Modified date

        public Dictionary<Guid, string> UpdatedItems { get; set; }

        public Guid RunningInstanceId { get; set; }
    }
}