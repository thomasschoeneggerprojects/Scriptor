using ScriptExecutor.Model;
using ScriptExecutorLib.Model.ErrorHandling;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ScriptExecutorLib.Model.Execution
{
    public sealed class ExecutionInformation
    {
        public DateTimeOffset? Start { get; }

        public DateTimeOffset? End { get; }

        public ExecutionState State { get; }

        public List<ScriptExecutorIssue> Issues { get; }

        private ExecutionInformation(DateTimeOffset start,
            DateTimeOffset end,
            ExecutionState state, params ScriptExecutorIssue[] issues)
        {
            Start = start;
            End = end;
            State = state;
        }

        private ExecutionInformation(DateTimeOffset start,
            ExecutionState state)
        {
            Start = start;
            State = state;
        }

        private ExecutionInformation(
           ExecutionState state)
        {
            State = state;
        }

        public static ExecutionInformation Nothing()
        {
            return new ExecutionInformation(ExecutionState.Nothing);
        }

        public static ExecutionInformation Aborted(DateTimeOffset start, params ScriptExecutorIssue[] issues)
        {
            DateTimeOffset end = DateTimeOffset.Now;
            var severity = CalculateSeverity(issues);

            if (severity == ScriptExecutorIssueSeverityIds.Warning)
            {
                return new ExecutionInformation(start, end, ExecutionState.AbortedWithWarning, issues);
            }

            if (severity == ScriptExecutorIssueSeverityIds.Error || severity == ScriptExecutorIssueSeverityIds.FatalError)
            {
                return new ExecutionInformation(start, end, ExecutionState.AbortedWithError, issues);
            }

            return new ExecutionInformation(start, end, ExecutionState.Aborted, issues);
        }

        public static ExecutionInformation Finished(DateTimeOffset start, params ScriptExecutorIssue[] issues)
        {
            DateTimeOffset end = DateTimeOffset.Now;

            var severity = CalculateSeverity(issues);

            if (severity == ScriptExecutorIssueSeverityIds.Warning)
            {
                return new ExecutionInformation(start, end, ExecutionState.FinishedWithWarning, issues);
            }

            if (severity == ScriptExecutorIssueSeverityIds.Error || severity == ScriptExecutorIssueSeverityIds.FatalError)
            {
                return new ExecutionInformation(start, end, ExecutionState.FinishedWithError, issues);
            }

            return new ExecutionInformation(start, end, ExecutionState.FinishedWithSuccess, issues);
        }

        public static ExecutionInformation Finished(DateTimeOffset start, ExecutionState state, params ScriptExecutorIssue[] issues)
        {
            DateTimeOffset end = DateTimeOffset.Now;
            return new ExecutionInformation(start, end, state);
        }

        private static ScriptExecutorIssueSeverityId CalculateSeverity(params ScriptExecutorIssue[] issues)
        {
            int highestPriority = 0;
            foreach (var issue in issues)
            {
                var currentPriority = issue.SeverityId.GetPriority();
                if (currentPriority > highestPriority)
                    highestPriority = currentPriority;
            }

            if (highestPriority <= ScriptExecutorIssueSeverityIds.Info.GetPriority())
            {
                return ScriptExecutorIssueSeverityIds.Info;
            }

            if (highestPriority == ScriptExecutorIssueSeverityIds.Warning.GetPriority())
            {
                return ScriptExecutorIssueSeverityIds.Warning;
            }

            if (highestPriority == ScriptExecutorIssueSeverityIds.Error.GetPriority())
            {
                return ScriptExecutorIssueSeverityIds.Error;
            }

            if (highestPriority == ScriptExecutorIssueSeverityIds.FatalError.GetPriority())
            {
                return ScriptExecutorIssueSeverityIds.FatalError;
            }

            throw new InvalidOperationException($"Unsupported severity with priority {highestPriority}");
        }
    }
}