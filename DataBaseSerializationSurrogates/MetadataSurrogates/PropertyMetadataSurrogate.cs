using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class PropertyMetadataSurrogate
    {
        #region Constructor

        public PropertyMetadataSurrogate( PropertyMetadata propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = GetTypesMetadata( propertyMetadata.PropertyAttributes ) as ICollection<TypeMetadataSurrogate>;
            _modifiers = propertyMetadata.Modifiers;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( propertyMetadata.TypeMetadata );
            PropertyInfo = propertyMetadata.PropertyInfo;
            Getter = propertyMetadata.Getter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Getter );
            Setter = propertyMetadata.Setter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Setter );
        }

        #endregion

        #region Properties

        public int PropertyId { get; set; }
        public string Name { get; set; }
        public ICollection<TypeMetadataSurrogate> PropertyAttributes { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public PropertyInfo PropertyInfo { get; set; }
        public MethodMetadataSurrogate Getter { get; set; }
        public MethodMetadataSurrogate Setter { get; set; }
        private Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> _modifiers;

        #region Modifiers

        public AccessLevel AccessLevel
        {
            get => _modifiers.Item1;
            set => _modifiers =
                new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( value, _modifiers.Item2, _modifiers.Item3,
                    _modifiers.Item4 );
        }
        public AbstractEnum AbstractEnum
        {
            get => _modifiers.Item2;
            set => _modifiers =
                new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( _modifiers.Item1, value, _modifiers.Item3,
                    _modifiers.Item4 );
        }
        public StaticEnum StaticEnum
        {
            get => _modifiers.Item3;
            set => _modifiers =
                new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( _modifiers.Item1, _modifiers.Item2, value,
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

        public ICollection<TypeMetadataSurrogate> TypePropertiesSurrogates { get; set; }

        #endregion

        public PropertyMetadata GetOriginalPropertyMetadata()
        {
            return new PropertyMetadata()
            {
                Name = Name,
                PropertyAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( PropertyAttributes ),
                Modifiers = _modifiers,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                PropertyInfo = PropertyInfo,
                Getter = Getter?.GetOriginalMethodMetadata(),
                Setter = Setter?.GetOriginalMethodMetadata()
            };
        }
    }
}