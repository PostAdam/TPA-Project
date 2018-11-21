using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( IsReference = true, Name = "FieldMetadata" )]
    public class FieldMetadataSurrogate
    {
        public FieldMetadataSurrogate( FieldMetadata fieldMetadata )
        {
            Name = fieldMetadata.Name;
            TypeMetadata = fieldMetadata.TypeMetadata;
            IsStatic = fieldMetadata.IsStatic;
            FieldAttributes = fieldMetadata.FieldAttributes;
        }

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeMetadata TypeMetadata { get; set; }

        [DataMember]
        public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }

        [DataMember]
        public StaticEnum IsStatic { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> FieldAttributes { get; set; }

        #endregion
    }
}