using System.Collections.Generic;
using System.Runtime.Serialization;
using ModelBase;
using ModelBase.Enums;
using static XmlSerializationSurrogates.CollectionOriginalTypeAccessor;
using static XmlSerializationSurrogates.CollectionTypeAccessor;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( IsReference = true, Name = "ParameterReflector" )]
    public class ParameterMetadataSurrogate
    {
        #region Constructor

        public ParameterMetadataSurrogate( ParameterMetadataBase parameterMetadata )
        {
            Name = parameterMetadata.Name;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( parameterMetadata.TypeMetadata );
            Position = parameterMetadata.Position;
            Kind = parameterMetadata.Kind;
            ParameterAttributes = GetTypesMetadata( parameterMetadata.ParameterAttributes );
            DefaultValue = parameterMetadata.DefaultValue;
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeMetadataSurrogate TypeMetadata { get; set; }

        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public ParameterKindEnum Kind { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> ParameterAttributes { get; set; }

        [DataMember]
        public string DefaultValue { get; set; }

        #endregion

        public ParameterMetadataBase GetOriginalParameterMetadata()
        {
            return new ParameterMetadataBase()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Position = Position,
                Kind = Kind,
                ParameterAttributes = GetOriginalTypesMetadata( ParameterAttributes ),
                DefaultValue = DefaultValue
            };
        }
    }
}