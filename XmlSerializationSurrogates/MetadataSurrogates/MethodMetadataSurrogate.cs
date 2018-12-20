using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ModelBase;
using ModelBase.Enums;
using static XmlSerializationSurrogates.CollectionOriginalTypeAccessor;
using static XmlSerializationSurrogates.CollectionTypeAccessor;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( IsReference = true, Name = "MethodReflector" )]
    public class MethodMetadataSurrogate
    {
        #region Constructor

        public MethodMetadataSurrogate( MethodMetadataBase methodMetadata )
        {
            Name = Name;
            Extension = methodMetadata.Extension;
            ReturnType = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( methodMetadata.ReturnType );
            MethodAttributes = GetTypesMetadata( methodMetadata.MethodAttributes );
            Parameters = GetParametersMetadata( methodMetadata.Parameters );
            GenericArguments = GetTypesMetadata( methodMetadata.GenericArguments );
            Modifiers = methodMetadata.Modifiers;
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public bool Extension { get; set; }

        [DataMember]
        public TypeMetadataSurrogate ReturnType { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> MethodAttributes { get; set; }

        [DataMember]
        public IEnumerable<ParameterMetadataSurrogate> Parameters { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> GenericArguments { get; set; }

        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        #endregion

        public MethodMetadataBase GetOriginalMethodMetadata()
        {
            return new MethodMetadataBase()
            {
                Name = Name,
                Extension = Extension,
                ReturnType = ReturnType?.EmitOriginalTypeMetadata(),
                MethodAttributes = GetOriginalTypesMetadata( MethodAttributes ),
                Parameters = GetOriginalParametersMetadata( Parameters ),
                GenericArguments = GetOriginalTypesMetadata( GenericArguments ),
                Modifiers = Modifiers
            };
        }
    }
}