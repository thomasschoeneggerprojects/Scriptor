using System;
using System.Globalization;
using System.Security.Principal;

namespace ScriptExecutor.ViewModel
{
    /// <summary>
    /// This type represents a strong typed identiy of a <see cref="ExecutionItemArgumentId"/>.
    /// </summary>
    public struct ExecutionItemArgumentId :
            IEquatable<ExecutionItemArgumentId>
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

        public ExecutionItemArgumentId(Guid guid)
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
            return other is ExecutionItemArgumentId && Equals((ExecutionItemArgumentId)other);
        }

        /// <inheritdoc/>
        public Boolean Equals(ExecutionItemArgumentId other)
        {
            if (_cachedHashCode != other._cachedHashCode)
            {
                return false;
            }

            return _guid.Equals(other._guid);
        }

        /// <inheritdoc/>
        public static Boolean operator ==(ExecutionItemArgumentId a, ExecutionItemArgumentId b)
        {
            return a.Equals(b);
        }

        /// <inheritdoc/>
        public static Boolean operator !=(ExecutionItemArgumentId a, ExecutionItemArgumentId b)
        {
            return !a.Equals(b);
        }
    }
}