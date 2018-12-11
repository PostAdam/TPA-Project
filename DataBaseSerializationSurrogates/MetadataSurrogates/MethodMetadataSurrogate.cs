using System;
using System.Collections.Generic;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class MethodMetadataSurrogate
    {
        #region Constructors

        public MethodMetadataSurrogate()
        {
        }

        public MethodMetadataSurrogate( MethodMetadata methodMetadata )
        {
            Name = Name;
            Extension = methodMetadata.Extension;
            ReturnType = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( methodMetadata.ReturnType );

            IEnumerable<TypeMetadataSurrogate> methodAttributes =  GetTypesMetadata( methodMetadata.MethodAttributes );
            MethodAttributes = methodAttributes == null ? null : new List<TypeMetadataSurrogate>( methodAttributes );

            IEnumerable<ParameterMetadataSurrogate> parameters =  GetParametersMetadata( methodMetadata.Parameters );
            Parameters =  parameters == null ? null : new List<ParameterMetadataSurrogate>(parameters);

            IEnumerable<TypeMetadataSurrogate> genericArguments =  GetTypesMetadata( methodMetadata.GenericArguments );
            GenericArguments = genericArguments == null ? null : new List<TypeMetadataSurrogate>( genericArguments );

            _modifiers = methodMetadata.Modifiers;
        }

        #endregion

        #region Properties

        public int MethodId { get; set; }
        public string Name { get; set; }
        public bool Extension { get; set; }
        public TypeMetadataSurrogate ReturnType { get; set; }
        public ICollection<TypeMetadataSurrogate> MethodAttributes { get; set; }
        public ICollection<ParameterMetadataSurrogate> Parameters { get; set; }
        public ICollection<TypeMetadataSurrogate> GenericArguments { get; set; }
        private Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> _modifiers;

        #region Modifiers

        public AccessLevel AccessLevel
        {
            get => _modifiers.Item1;
            set => _modifiers =
                new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( value, _modifiers.Item2,
                    _modifiers.Item3,
                    _modifiers.Item4 );
        }

        public AbstractEnum AbstractEnum
        {
            get => _modifiers.Item2;
            set => _modifiers =
                new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( _modifiers.Item1, value,
                    _modifiers.Item3,
                    _modifiers.Item4 );
        }

        public StaticEnum StaticEnum
        {
            get => _modifiers.Item3;
            set => _modifiers =
                new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( _modifiers.Item1, _modifiers.Item2,
                    value,
                    _modifiers.Item4 );
        }

        public VirtualEnum VirtualEnum
        {
            get => _modifiers.Item4;
            set => _modifiers =
                new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( _modifiers.Item1, _modifiers.Item2,
                    _modifiers.Item3, value );
        }

        #endregion

        #endregion

        #region Navigation Properties

        public int ReturnTypeId { get; set; }

        public ICollection<EventMetadataSurrogate> EventAddMethodMetadataSurrogates { get; set; }
        public ICollection<EventMetadataSurrogate> EventRaiseMethodMetadataSurrogates { get; set; }
        public ICollection<EventMetadataSurrogate> EventRemoveMethodMetadataSurrogates { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeMethodMetadataSurrogates { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeConstructorMetadataSurrogates { get; set; }
        public ICollection<PropertyMetadataSurrogate> PropertyGettersSurrogates { get; set; }
        public ICollection<PropertyMetadataSurrogate> PropertySettersSurrogates { get; set; }

        #endregion

        public MethodMetadata GetOriginalMethodMetadata()
        {
            return new MethodMetadata()
            {
                Name = Name,
                Extension = Extension,
                ReturnType = ReturnType?.EmitOriginalTypeMetadata(),
                MethodAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( MethodAttributes ),
                Parameters = CollectionOriginalTypeAccessor.GetOriginalParametersMetadata( Parameters ),
                GenericArguments = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( GenericArguments ),
                Modifiers = _modifiers
            };
        }
    }
}