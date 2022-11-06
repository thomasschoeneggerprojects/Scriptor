using System;
using System.Globalization;
using System.Security.Principal;

namespace ScriptExecutorLib.Model.Execution
{
    /// <summary>
    /// This type represents a strong typed identiy of a <see cref="ExecutionItemId"/>.
    /// </summary>
    public struct ExecutionItemId :
            IEquatable<ExecutionItemId>
    {
        private readonly Guid _guid;

        private readonly int _cachedHashCode;

        /// <summary>
        /// Returns the Guid that is being wrapped.
        /// </summary>
        public Guid Guid => _guid;

        /// <summary>
        /// New instance.
        /// </summary>

        public ExecutionItemId(Guid guid)
        {
            _guid = guid;
            _cachedHashCode = _guid.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return _guid.ToString("B", CultureInfo.InvariantCulture).ToUpperInvariant();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return _cachedHashCode;
        }

        /// <inheritdoc/>
        public override bool Equals(object other)
        {
            return other is ExecutionItemId && Equals((ExecutionItemId)other);
        }

        /// <inheritdoc/>
        public bool Equals(ExecutionItemId other)
        {
            if (_cachedHashCode != other._cachedHashCode)
            {
                return false;
            }

            return _guid.Equals(other._guid);
        }

        /// <inheritdoc/>
        public static bool operator ==(ExecutionItemId a, ExecutionItemId b)
        {
            return a.Equals(b);
        }

        /// <inheritdoc/>
        public static bool operator !=(ExecutionItemId a, ExecutionItemId b)
        {
            return !a.Equals(b);
        }
    }
}