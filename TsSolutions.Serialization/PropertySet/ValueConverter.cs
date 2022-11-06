using System;
using System.Collections.Generic;
using System.Text;

namespace TsSolutions.Serialization.PropertySet
{
    public class ValueConverter<D, E> : IValueConverter<D, E>
    {
        private ValueConverter(Func<E, D> mapFunctionToDto,
            Func<D, E> mapFunctionToEntity)
        {
            MapToDto = mapFunctionToDto;
            MapToEntity = mapFunctionToEntity;

            DtoType = typeof(D);
            EntityType = typeof(E);
        }

        internal Type DtoType { get; }
        internal Type EntityType { get; }

        internal Func<E, D> MapToDto { get; }
        internal Func<D, E> MapToEntity { get; }

        internal static ValueConverter<D, E> Create(Func<E, D> mapFunctionToDto,
            Func<D, E> mapFunctionToEntity)
        {
            return new ValueConverter<D, E>(mapFunctionToDto, mapFunctionToEntity);
        }
    }
}