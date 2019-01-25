using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static DataBaseSerializationSurrogates.CollectionOriginalTypeAccessor;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class MethodMetadataSurrogate
    {
        #region Constructors

        public MethodMetadataSurrogate()
        {
        }

        public MethodMetadataSurrogate( MethodMetadataBase methodMetadata )
        {
            Name = methodMetadata.Name;
            Extension = methodMetadata.Extension;
            ReturnType = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( methodMetadata.ReturnType );

            IEnumerable<TypeMetadataSurrogate> methodAttributes =  GetTypesMetadata( methodMetadata.MethodAttributes );
            MethodAttributes = methodAttributes == null ? null : new List<TypeMetadataSurrogate>( methodAttributes );
            Parameters = GetParametersMetadata( methodMetadata.Parameters );
            GenericArguments = GetTypesMetadata( methodMetadata.GenericArguments );
            _modifiers = methodMetadata.Modifiers;
        }

        #endregion

        #region Properties

        public int? MethodId { get; set; }
        public string Name { get; set; }
        public bool Extension { get; set; }
        public TypeMetadataSurrogate ReturnType { get; set; }
        public ICollection<TypeMetadataSurrogate> MethodAttributes { get; set; }
        public ICollection<ParameterMetadataSurrogate> Parameters { get; set; }
        public ICollection<TypeMetadataSurrogate> GenericArguments { get; set; }
        private Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> _modifiers;

        #region Modifiers

        public AccessLevel? AccessLevel
        {
            get => _modifiers?.Item1;
            set
            {
                if ( value != null )
                    _modifiers =
                        new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( 
                            value.Value,
                            _modifiers?.Item2 ?? ModelBase.Enums.AbstractEnum.NotAbstract,
                            _modifiers?.Item3 ?? ModelBase.Enums.StaticEnum.NotStatic,
                            _modifiers?.Item4 ?? ModelBase.Enums.VirtualEnum.NotVirtual );
            }
        }

        public AbstractEnum? AbstractEnum
        {
            get => _modifiers?.Item2;
            set
            {
                if ( value != null )
                    _modifiers =
                        new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(
                            _modifiers?.Item1 ?? ModelBase.Enums.AccessLevel.Public,
                            value.Value,
                            _modifiers?.Item3 ?? ModelBase.Enums.StaticEnum.NotStatic,
                            _modifiers?.Item4 ?? ModelBase.Enums.VirtualEnum.NotVirtual );
            }
        }

        public StaticEnum? StaticEnum
        {
            get => _modifiers?.Item3;
            set
            {
                if ( value != null )
                    _modifiers =
                        new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(
                            _modifiers?.Item1 ?? ModelBase.Enums.AccessLevel.Public,
                            _modifiers?.Item2 ?? ModelBase.Enums.AbstractEnum.NotAbstract,
                            value.Value,
                            _modifiers?.Item4 ?? ModelBase.Enums.VirtualEnum.NotVirtual );
            }
        }

        public VirtualEnum? VirtualEnum
        {
            get => _modifiers?.Item4;
            set
            {
                if ( value != null )
                    _modifiers =
                        new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(
                            _modifiers?.Item1 ?? ModelBase.Enums.AccessLevel.Public,
                            _modifiers?.Item2 ?? ModelBase.Enums.AbstractEnum.NotAbstract,
                            _modifiers?.Item3 ?? ModelBase.Enums.StaticEnum.NotStatic,
                            value.Value );
            }
        }

        #endregion

        #endregion

        #region Navigation Properties

        public int? ReturnTypeId { get; set; }
        public ICollection<EventMetadataSurrogate> EventAddMethodSurrogates { get; set; }
        public ICollection<EventMetadataSurrogate> EventRaiseMethodSurrogates { get; set; }
        public ICollection<EventMetadataSurrogate> EventRemoveMethodSurrogates { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeMethodSurrogates { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeConstructorSurrogates { get; set; }
        public ICollection<PropertyMetadataSurrogate> PropertyGettersSurrogates { get; set; }
        public ICollection<PropertyMetadataSurrogate> PropertySettersSurrogates { get; set; }

        #endregion

        public MethodMetadataBase GetOriginalMethodMetadata()
        {
            return new MethodMetadataBase
            {
                Name = Name,
                Extension = Extension,
                ReturnType = ReturnType?.EmitOriginalTypeMetadata(),
                MethodAttributes = GetOriginalTypesMetadata( MethodAttributes ),
                Parameters = GetOriginalParametersMetadata( Parameters ),
                GenericArguments = GetOriginalTypesMetadata( GenericArguments ),
                Modifiers = _modifiers
            };
        }
    }
}