using ScriptExecutor.Model;
using System;
using System.Collections.Generic;

namespace ScriptExecutorLib.Model.ErrorHandling
{
    public static class ScriptExecutorIssueSeverityIds
    {
        public static ScriptExecutorIssueSeverityId Info { get; }
            = new ScriptExecutorIssueSeverityId(Guid.Parse("21f8d924-e696-4a00-b53d-3d68ecc03ddf"));

        public static ScriptExecutorIssueSeverityId Warning { get; }
            = new ScriptExecutorIssueSeverityId(Guid.Parse("1516d727-e92a-4171-8bb3-accc9af4634e"));

        public static ScriptExecutorIssueSeverityId Error { get; }
            = new ScriptExecutorIssueSeverityId(Guid.Parse("7f7d3e56-0b1c-4dc2-9e40-e9e658092a2a"));

        public static ScriptExecutorIssueSeverityId FatalError { get; }
            = new ScriptExecutorIssueSeverityId(Guid.Parse("b4f26026-f2e1-4433-9e2c-b84d2923932e"));

        private static Dictionary<ScriptExecutorIssueSeverityId, int> _priorities
            = new Dictionary<ScriptExecutorIssueSeverityId, int>
        {
            {Info, 10 },
            {Warning, 20 },
            {Error, 30 },
            {FatalError, 40 }
        };

        public static int GetPriority(this ScriptExecutorIssueSeverityId scriptExecutorIssueSeverityId)
        {
            return _priorities[scriptExecutorIssueSeverityId];
        }
    }
}