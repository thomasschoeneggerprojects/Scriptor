using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Configuration
{
    internal class GlobalConstants
    {
        public const string APPLICATION_FOLDER_NAME = "ScriptExecutorLib";

        public const string APPLICATION_EXECUTIONITEM_FOLDER_NAME = "executionItem";

        public const string EXECUTIONITEM_FILE_EXTENSION = "exi";

        private static readonly string APPLICATION_RELATIVE_DATA_STORAGE_PATH = Path.Combine(APPLICATION_FOLDER_NAME, "data");

        internal static string DefaultExecutionItemEntryFolderPath
        {
            get
            {
                string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                var defaultFolderPath = Path.Combine(folderPath, APPLICATION_RELATIVE_DATA_STORAGE_PATH, APPLICATION_EXECUTIONITEM_FOLDER_NAME);
                return defaultFolderPath;
            }
        }
    }
}