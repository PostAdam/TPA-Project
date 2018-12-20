using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
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

        public string Name { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public int Position { get; set; }
        public ParameterKindEnum Kind { get; set; }
        public IEnumerable<TypeMetadataSurrogate> ParameterAttributes { get; set; }
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