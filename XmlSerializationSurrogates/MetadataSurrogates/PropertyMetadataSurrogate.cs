using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ModelBase;
using ModelBase.Enums;
using static XmlSerializationSurrogates.CollectionOriginalTypeAccessor;
using static XmlSerializationSurrogates.CollectionTypeAccessor;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( IsReference = true, Name = "PropertyReflector" )]
    public class PropertyMetadataSurrogate
    {
        #region Constructor

        public PropertyMetadataSurrogate( PropertyMetadataBase propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = GetTypesMetadata( propertyMetadata.PropertyAttributes );
            Modifiers = propertyMetadata.Modifiers;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( propertyMetadata.TypeMetadata );
            Getter = propertyMetadata.Getter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Getter );
            Setter = propertyMetadata.Setter == null ? null : new MethodMetadataSurrogate( propertyMetadata.Setter );
        }

        #endregion

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
        public MethodMetadataSurrogate Getter { get; set; }

        [DataMember]
        public MethodMetadataSurrogate Setter { get; set; }

        #endregion

        public PropertyMetadataBase GetOriginalPropertyMetadata()
        {
            return new PropertyMetadataBase()
            {
                Name = Name,
                PropertyAttributes = GetOriginalTypesMetadata( PropertyAttributes ),
                Modifiers = Modifiers,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Getter = Getter?.GetOriginalMethodMetadata(),
                Setter = Setter?.GetOriginalMethodMetadata()
            };
        }
    }
}