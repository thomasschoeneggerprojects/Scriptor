using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.Execution
{
    public enum ExecutionState
    {
        Nothing,
        Queueing,
        Running,
        Pausing,
        Finished,
        FinishedWithSuccess,
        FinishedWithWarning,
        FinishedWithError,
        Cancelled,
        Aborted,
        AbortedWithWarning,
        AbortedWithError
    }
}