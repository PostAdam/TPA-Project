using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class ParameterMetadataSurrogate
    {
        #region Constructor

        public ParameterMetadataSurrogate( ParameterMetadata parameterMetadata )
        {
            Name = parameterMetadata.Name;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( parameterMetadata.TypeMetadata );
            Position = parameterMetadata.Position;
            Kind = parameterMetadata.Kind;
            ParameterAttributes = CollectionTypeAccessor.GetTypesMetadata( parameterMetadata.ParameterAttributes );
            DefaultValue = parameterMetadata.DefaultValue;
        }

        #endregion

        #region Properties

        [Key]
        public string Name { get; set; }
        
        public TypeMetadataSurrogate TypeMetadata { get; set; }

        public int Position { get; set; }

        public ParameterKindEnum Kind { get; set; }

        public IEnumerable<TypeMetadataSurrogate> ParameterAttributes { get; set; }

        public string DefaultValue { get; set; }

        #endregion

        public ParameterMetadata GetOriginalParameterMetadata()
        {
            return new ParameterMetadata()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Position = Position,
                Kind = Kind,
                ParameterAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( ParameterAttributes ),
                DefaultValue = DefaultValue
            };
        }
    }
}