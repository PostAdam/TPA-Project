using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( IsReference = true, Name = "PropertyMetadata" )]
    public class PropertyMetadataSurrogate
    {

        public PropertyMetadataSurrogate( PropertyMetadata propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = propertyMetadata.PropertyAttributes;
            Modifiers = Modifiers;
            TypeMetadata = propertyMetadata.TypeMetadata;
            PropertyInfo = propertyMetadata.PropertyInfo;
            Getter = propertyMetadata.Getter;
            Setter = propertyMetadata.Setter;
        }

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> PropertyAttributes { get; set; }

        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        [DataMember]
        public TypeMetadata TypeMetadata { get; set; }

        [DataMember]
        public PropertyInfo PropertyInfo { get; set; }

        [DataMember]
        public MethodMetadata Getter { get; set; }

        [DataMember]
        public MethodMetadata Setter { get; set; }

        #endregion
    }
}