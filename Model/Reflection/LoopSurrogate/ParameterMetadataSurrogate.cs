using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( IsReference = true, Name = "ParameterMetadata" )]
    public class ParameterMetadataSurrogate
    {

        public ParameterMetadataSurrogate( ParameterMetadata parameterMetadata )
        {
            Name = parameterMetadata.Name;
            TypeMetadata = parameterMetadata.TypeMetadata;
            Position = parameterMetadata.Position;
            Kind = parameterMetadata.Kind;
            ParameterAttributes = parameterMetadata.ParameterAttributes;
            DefaultValue = parameterMetadata.DefaultValue;
        }

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeMetadata TypeMetadata { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public ParameterKindEnum Kind { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> ParameterAttributes { get; set; }

        [DataMember]
        public string DefaultValue { get; set; }

        #endregion
    }
}