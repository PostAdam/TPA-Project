using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "PropertyMetadata" )]
    public class PropertyMetadataSurrogate
    {
        private readonly ReproducedTypes _reproducedTypes = ReproducedTypes.Instance;

        public PropertyMetadataSurrogate( PropertyMetadata propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = CollectionTypeAccessor.GetTypesMetadata( propertyMetadata.PropertyAttributes );
            Modifiers = Modifiers;
            TypeMetadata = _reproducedTypes.GetType( propertyMetadata.TypeMetadata )/*new TypeMetadata( propertyMetadata.TypeMetadata )*/;
            PropertyInfo = propertyMetadata.PropertyInfo;
            Getter = new MethodMetadataSurrogate( propertyMetadata.Getter );
            Setter = new MethodMetadataSurrogate( propertyMetadata.Setter );
        }

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> PropertyAttributes { get; set; }

        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        [DataMember]
        public TypeMetadataSurrogate TypeMetadata { get; set; }

        [DataMember]
        public PropertyInfo PropertyInfo { get; set; }

        [DataMember]
        public MethodMetadataSurrogate Getter { get; set; }

        [DataMember]
        public MethodMetadataSurrogate Setter { get; set; }

        #endregion

        public PropertyMetadata GetOryginalPropertyMetadata()
        {
            return new PropertyMetadata()
            {
                Name = Name,
                PropertyAttributes = CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( PropertyAttributes ),
                Modifiers = Modifiers,
                TypeMetadata = TypeMetadata.GetOryginalTypeMetadata(),
                PropertyInfo = PropertyInfo,
                Getter = Getter.GetOryginalMethodMetadata(),
                Setter = Setter.GetOryginalMethodMetadata()
            };
        }
    }
}