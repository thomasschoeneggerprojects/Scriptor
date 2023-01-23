using System;
using System.Globalization;
using System.Security.Principal;

namespace ScriptExecutorLib.Model.Execution
{
    /// <summary>
    /// This type represents a strong typed identiy of a <see cref="ExecutionCollectionId"/>.
    /// </summary>
    public struct ExecutionCollectionId :
            IEquatable<ExecutionCollectionId>
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

        public ExecutionCollectionId(Guid guid)
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
        public override bool Equals(object? other)
        {
            return other is ExecutionCollectionId && Equals((ExecutionCollectionId)other);
        }

        /// <inheritdoc/>
        public bool Equals(ExecutionCollectionId other)
        {
            if (_cachedHashCode != other._cachedHashCode)
            {
                return false;
            }

            return _guid.Equals(other._guid);
        }

        /// <inheritdoc/>
        public static bool operator ==(ExecutionCollectionId a, ExecutionCollectionId b)
        {
            return a.Equals(b);
        }

        /// <inheritdoc/>
        public static bool operator !=(ExecutionCollectionId a, ExecutionCollectionId b)
        {
            return !a.Equals(b);
        }
    }
}