using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static Model.Reflection.NewSurrogates.CollectionOryginalTypeAccessor;
using static Model.Reflection.NewSurrogates.CollectionTypeAccessor;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "MethodMetadata" )]
    public class MethodMetadataSurrogate
    {
        #region Constructor

        public MethodMetadataSurrogate( MethodMetadata methodMetadata )
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

        public MethodMetadata GetOryginalMethodMetadata()
        {
            return new MethodMetadata()
            {
                Name = Name,
                Extension = Extension,
                ReturnType = ReturnType?.EmitOriginalTypeMetadata(),
                MethodAttributes = GetOryginalTypesMetadata( MethodAttributes ),
                Parameters = GetOryginalParametersMetadata( Parameters ),
                GenericArguments = GetOryginalTypesMetadata( GenericArguments ),
                Modifiers = Modifiers
            };
        }
    }
}