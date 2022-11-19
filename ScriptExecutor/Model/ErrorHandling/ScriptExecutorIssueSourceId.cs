using System;
using System.Globalization;

namespace ScriptExecutor.Model
{
    /// <summary>
    /// This type represents a strong typed identiy of a <see cref="ScriptExecutorIssueSourceId"/>.
    /// </summary>
    public struct ScriptExecutorIssueSourceId :
            IEquatable<ScriptExecutorIssueSourceId>
    {
        private readonly Guid _guid;

        private readonly Int32 _cachedHashCode;

        /// <summary>
        /// Returns the Guid that is being wrapped.
        /// </summary>
        public Guid Guid => _guid;

        /// <summary>
        /// New instance.
        /// </summary>

        public ScriptExecutorIssueSourceId(Guid guid)
        {
            _guid = guid;
            _cachedHashCode = _guid.GetHashCode();
        }

        /// <inheritdoc/>
        public override String ToString()
        {
            return _guid.ToString("B", CultureInfo.InvariantCulture).ToUpperInvariant();
        }

        /// <inheritdoc/>
        public override Int32 GetHashCode()
        {
            return _cachedHashCode;
        }

        /// <inheritdoc/>
        public override Boolean Equals(Object other)
        {
            return other is ScriptExecutorIssueSourceId && Equals((ScriptExecutorIssueSourceId)other);
        }

        /// <inheritdoc/>
        public Boolean Equals(ScriptExecutorIssueSourceId other)
        {
            if (_cachedHashCode != other._cachedHashCode)
            {
                return false;
            }

            return _guid.Equals(other._guid);
        }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptExecutorIssueSourceId a, ScriptExecutorIssueSourceId b)
        {
            return a.Equals(b);
        }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptExecutorIssueSourceId a, ScriptExecutorIssueSourceId b)
        {
            return !a.Equals(b);
        }
    }
}