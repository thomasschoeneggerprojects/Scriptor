using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TsSolutions.Storage.FileStorage
{
    [System.Serializable]
    public class ItemOverviewInfo
    {
        public ItemOverviewInfo()
        {
            //Need for serialization
        }

        #region Modified date

        [XmlElement]
        public string LastModifiedDateForXml
        {
            get; set;
        }

        [XmlIgnore]
        public DateTimeOffset LastModifiedDate
        {
            get
            {
                return DateTimeOffset.Parse(LastModifiedDateForXml);
            }
            set
            {
                LastModifiedDateForXml = value.ToString("O");
            }
        }

        #endregion Modified date

        [XmlElement]
        public Guid ItemGuid
        {
            get; set;
        }

        [XmlElement]
        public string DisplayName
        {
            get; set;
        }

        [XmlElement]
        public string Description
        {
            get; set;
        }
    }
}