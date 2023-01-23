using System;
using System.Globalization;

namespace ScriptExecutor.Model
{
    /// <summary>
    /// This type represents a strong typed identiy of a <see cref="ScriptExecutorIssueId"/>.
    /// </summary>
    public struct ScriptExecutorIssueId :
            IEquatable<ScriptExecutorIssueId>
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

        internal ScriptExecutorIssueId(Guid guid)
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
        public override Boolean Equals(object? other)
        {
            return other is ScriptExecutorIssueId && Equals((ScriptExecutorIssueId)other);
        }

        /// <inheritdoc/>
        public Boolean Equals(ScriptExecutorIssueId other)
        {
            if (_cachedHashCode != other._cachedHashCode)
            {
                return false;
            }

            return _guid.Equals(other._guid);
        }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptExecutorIssueId a, ScriptExecutorIssueId b)
        {
            return a.Equals(b);
        }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptExecutorIssueId a, ScriptExecutorIssueId b)
        {
            return !a.Equals(b);
        }
    }
}