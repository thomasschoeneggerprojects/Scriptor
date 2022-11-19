using ScriptExecutor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutorLib.Model.ErrorHandling
{
    public class ScriptExecutorIssue
    {
        public ScriptExecutorIssueId IssueId { get; }

        public ScriptExecutorIssueSeverityId SeverityId { get; }

        public ScriptExecutorIssueSourceId SourceId { get; }

        // add Metadata

        private ScriptExecutorIssue(ScriptExecutorIssueId issueId,
            ScriptExecutorIssueSeverityId severityId,
            ScriptExecutorIssueSourceId sourceId)
        {
            IssueId = issueId;
            SeverityId = severityId;
            SourceId = sourceId;
        }

        public static ScriptExecutorIssue Create(ScriptExecutorIssueId issueId,
            ScriptExecutorIssueSeverityId severityId,
            ScriptExecutorIssueSourceId sourceId)
        {
            return new ScriptExecutorIssue(issueId, severityId, sourceId);
        }
    }
}