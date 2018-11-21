using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( IsReference = true, Name = "MethodMetadata" )]
    public class MethodMetadataSurrogate
    {
        public MethodMetadataSurrogate( MethodMetadata methodMetadata )
        {
            Name = Name;
            Extension = methodMetadata.Extension;
            ReturnType = methodMetadata.ReturnType;
            MethodAttributes = methodMetadata.MethodAttributes;
            Parameters = methodMetadata.Parameters;
            GenericArguments = methodMetadata.GenericArguments;
            Modifiers = methodMetadata.Modifiers;
        }

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool Extension { get; set; }

        [DataMember]
        public TypeMetadata ReturnType { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> MethodAttributes { get; set; }

        [DataMember]
        public IEnumerable<ParameterMetadata> Parameters { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }

        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        #endregion
    }
}