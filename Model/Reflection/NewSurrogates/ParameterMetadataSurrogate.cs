using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "ParameterMetadata" )]
    public class ParameterMetadataSurrogate
    {
        private readonly ReproducedTypes _reproducedTypes = ReproducedTypes.Instance;

        public ParameterMetadataSurrogate( ParameterMetadata parameterMetadata )
        {
            Name = parameterMetadata.Name;
            TypeMetadata = _reproducedTypes.GetType( parameterMetadata.TypeMetadata )/*new TypeMetadata( parameterMetadata.TypeMetadata )*/;
            Position = parameterMetadata.Position;
            Kind = parameterMetadata.Kind;
            ParameterAttributes = CollectionTypeAccessor.GetTypesMetadata( parameterMetadata.ParameterAttributes );
            DefaultValue = parameterMetadata.DefaultValue;
        }

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

        public ParameterMetadata GetOryginalParameterMetadata()
        {
            return new ParameterMetadata()
            {
                Name = Name,
                TypeMetadata = TypeMetadata.GetOryginalTypeMetadata(),
                Position = Position,
                Kind = Kind,
                ParameterAttributes = CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( ParameterAttributes ),
                DefaultValue = DefaultValue
            };
        }
    }
}