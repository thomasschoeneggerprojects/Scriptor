using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Execution
{
    public enum ExecutionItemType : byte
    {
        Information = 0,
        BatFile = 1,
        Powershell = 2,
    }
}