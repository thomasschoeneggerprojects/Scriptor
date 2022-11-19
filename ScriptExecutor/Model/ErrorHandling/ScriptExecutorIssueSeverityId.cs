using System;
using System.Globalization;
using System.Security.Principal;

namespace ScriptExecutor.Model
{
    /// <summary>
    /// This type represents a strong typed identiy of a <see cref="ScriptExecutorIssueSeverityId"/>.
    /// </summary>
    public struct ScriptExecutorIssueSeverityId :
            IEquatable<ScriptExecutorIssueSeverityId>
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

        internal ScriptExecutorIssueSeverityId(Guid guid)
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
            return other is ScriptExecutorIssueSeverityId && Equals((ScriptExecutorIssueSeverityId)other);
        }

        /// <inheritdoc/>
        public Boolean Equals(ScriptExecutorIssueSeverityId other)
        {
            if (_cachedHashCode != other._cachedHashCode)
            {
                return false;
            }

            return _guid.Equals(other._guid);
        }

        /// <inheritdoc/>
        public static Boolean operator ==(ScriptExecutorIssueSeverityId a, ScriptExecutorIssueSeverityId b)
        {
            return a.Equals(b);
        }

        /// <inheritdoc/>
        public static Boolean operator !=(ScriptExecutorIssueSeverityId a, ScriptExecutorIssueSeverityId b)
        {
            return !a.Equals(b);
        }
    }
}