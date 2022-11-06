using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Storage.FileStorage
{
    public static class JsonMapper
    {
        public static string Map(DateTimeOffset dateTimeOffset)
        {
            return dateTimeOffset.ToString("O");
        }

        public static DateTimeOffset MapToDateTimeOffset(string dateTimeOffset)
        {
            var dt = DateTimeOffset.Parse(dateTimeOffset);
            return dt;
        }
    }
}