using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static DataBaseSerializationSurrogates.CollectionOriginalTypeAccessor;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class PropertyMetadataSurrogate 
    {
        #region Constructors

        public PropertyMetadataSurrogate()
        {
        }

        public PropertyMetadataSurrogate( PropertyMetadataBase propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = GetTypesMetadata( propertyMetadata.PropertyAttributes );
            _modifiers = propertyMetadata.Modifiers;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( propertyMetadata.TypeMetadata );
            Getter = propertyMetadata.Getter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Getter );
            Setter = propertyMetadata.Setter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Setter );
        }

        #endregion

        #region Properties

        public int? PropertyId { get; set; }
        public string Name { get; set; }
        public ICollection<TypeMetadataSurrogate> PropertyAttributes { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public MethodMetadataSurrogate Getter { get; set; }
        public MethodMetadataSurrogate Setter { get; set; }
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

        public int? TypeForeignId { get; set; }
        public int? GetterId { get; set; }
        public int? SetterId { get; set; }
        public ICollection<TypeMetadataSurrogate> TypePropertiesSurrogates { get; set; }

        #endregion

        public PropertyMetadataBase GetOriginalPropertyMetadata()
        {
            return new PropertyMetadataBase()
            {
                Name = Name,
                PropertyAttributes = GetOriginalTypesMetadata( PropertyAttributes ),
                Modifiers = _modifiers,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Getter = Getter?.GetOriginalMethodMetadata(),
                Setter = Setter?.GetOriginalMethodMetadata()
            };
        }
    }
}