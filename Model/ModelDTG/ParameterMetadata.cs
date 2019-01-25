using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.MetadataModels;
using ModelBase;
using Model.Reflection.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class ParameterMetadata
    {
        #region Constructor
        public ParameterMetadata()
        {
        }

        internal ParameterMetadata( ParameterInfo parameterInfo )
        {
            Name = parameterInfo.Name;
            TypeMetadata = TypeReflector.EmitType( parameterInfo.ParameterType );
            ParameterAttributes = TypeReflector.EmitAttributes( parameterInfo.CustomAttributes );
            Kind = ParameterReflector.GetParameterKind( parameterInfo );
            Position = parameterInfo.Position;
        }

        public ParameterMetadata( ParameterMetadataBase parameterMetadata )
        {
            Name = parameterMetadata.Name;
            TypeMetadata = TypeMetadata.EmitSurrogateTypeMetadata( parameterMetadata.TypeMetadata );
            Position = parameterMetadata.Position;
            Kind = (ParameterKindEnum) parameterMetadata.Kind;
            ParameterAttributes = GetTypesMetadata( parameterMetadata.ParameterAttributes );
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
        public int Position { get; set; }
        public ParameterKindEnum Kind { get; set; }
        public IEnumerable<TypeMetadata> ParameterAttributes { get; set; }

        #endregion

        public ParameterMetadataBase GetOriginalParameterMetadata()
        {
            return new ParameterMetadataBase()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Position = Position,
                Kind = (ModelBase.Enums.ParameterKindEnum) Kind,
                ParameterAttributes = GetOriginalTypesMetadata( ParameterAttributes ),
            };
        }
    }
}