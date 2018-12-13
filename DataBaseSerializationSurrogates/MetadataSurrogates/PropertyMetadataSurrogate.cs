using System;
using System.Collections.Generic;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
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

        public PropertyMetadataSurrogate( PropertyMetadata propertyMetadata )
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
                            _modifiers?.Item2 ?? Model.Reflection.Enums.AbstractEnum.NotAbstract,
                            _modifiers?.Item3 ?? Model.Reflection.Enums.StaticEnum.NotStatic,
                            _modifiers?.Item4 ?? Model.Reflection.Enums.VirtualEnum.NotVirtual );
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
                            _modifiers?.Item1 ?? Model.Reflection.Enums.AccessLevel.Public,
                            value.Value,
                            _modifiers?.Item3 ?? Model.Reflection.Enums.StaticEnum.NotStatic,
                            _modifiers?.Item4 ?? Model.Reflection.Enums.VirtualEnum.NotVirtual );
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
                            _modifiers?.Item1 ?? Model.Reflection.Enums.AccessLevel.Public,
                            _modifiers?.Item2 ?? Model.Reflection.Enums.AbstractEnum.NotAbstract,
                            value.Value,
                            _modifiers?.Item4 ?? Model.Reflection.Enums.VirtualEnum.NotVirtual );
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
                            _modifiers?.Item1 ?? Model.Reflection.Enums.AccessLevel.Public,
                            _modifiers?.Item2 ?? Model.Reflection.Enums.AbstractEnum.NotAbstract,
                            _modifiers?.Item3 ?? Model.Reflection.Enums.StaticEnum.NotStatic,
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

        public PropertyMetadata GetOriginalPropertyMetadata()
        {
            return new PropertyMetadata()
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