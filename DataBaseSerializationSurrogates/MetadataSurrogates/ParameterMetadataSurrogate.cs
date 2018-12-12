using System.Collections.Generic;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static DataBaseSerializationSurrogates.CollectionOriginalTypeAccessor;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class ParameterMetadataSurrogate
    {
        #region Constructors

        public ParameterMetadataSurrogate()
        {
        }

        public ParameterMetadataSurrogate( ParameterMetadata parameterMetadata )
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

        public int? ParameterId { get; set; }
        public string Name { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public int Position { get; set; }
        public ParameterKindEnum Kind { get; set; }
        public ICollection<TypeMetadataSurrogate> ParameterAttributes { get; set; }
        public string DefaultValue { get; set; }

        #endregion

        #region Navigation Properties

        public int? TypeForeignId { get; set; }
        public ICollection<MethodMetadataSurrogate> MethodParametersSurrogates { get; set; }

        #endregion

        public ParameterMetadata GetOriginalParameterMetadata()
        {
            return new ParameterMetadata()
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