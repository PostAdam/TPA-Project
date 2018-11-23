using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static Model.Reflection.NewSurrogates.CollectionOryginalTypeAccessor;
using static Model.Reflection.NewSurrogates.CollectionTypeAccessor;

namespace Model.Reflection.NewSurrogates
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
            PropertyInfo = propertyMetadata.PropertyInfo;
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
                PropertyAttributes = GetOryginalTypesMetadata( PropertyAttributes ),
                Modifiers = Modifiers,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                PropertyInfo = PropertyInfo,
                Getter = Getter?.GetOryginalMethodMetadata(),
                Setter = Setter?.GetOryginalMethodMetadata()
            };
        }
    }
}