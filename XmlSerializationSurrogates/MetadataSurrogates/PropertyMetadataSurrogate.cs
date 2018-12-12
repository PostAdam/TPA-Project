using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static XmlSerializationSurrogates.CollectionTypeAccessor;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( IsReference = true, Name = "PropertyMetadata" )]
    public class PropertyMetadataSurrogate
    {
        #region Constructor

        public PropertyMetadataSurrogate( PropertyMetadata propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = GetTypesMetadata( propertyMetadata.PropertyAttributes );
            Modifiers = Modifiers;
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

        public PropertyMetadata GetOriginalPropertyMetadata()
        {
            return new PropertyMetadata()
            {
                Name = Name,
                PropertyAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( PropertyAttributes ),
                Modifiers = Modifiers,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Getter = Getter?.GetOriginalMethodMetadata(),
                Setter = Setter?.GetOriginalMethodMetadata()
            };
        }
    }
}