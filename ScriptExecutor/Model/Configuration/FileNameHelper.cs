using ScriptExecutorLib.Model.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Configuration
{
    internal class FileNameHelper
    {
        /// <summary>
        /// Execution item file extension.
        /// </summary>
        public static string ExecutionItemFileExtension { get; } = GlobalConstants.EXECUTIONITEM_FILE_EXTENSION;

        internal static string GetFileName(ExecutionItem item)
        {
            return $"{item.Id.Guid}.{ExecutionItemFileExtension}";
        }

        internal static bool IsExecutionItemFile(string filepath)
        {
            return filepath.EndsWith(ExecutionItemFileExtension);
        }
    }
}