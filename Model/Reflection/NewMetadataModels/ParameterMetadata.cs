using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
{
    public class ParameterMetadata : ParameterMetadataBase
    {
        #region Constructors

        public ParameterMetadata()
        {
        }

        internal ParameterMetadata( ParameterInfo parameterInfo )
        {
            Name = parameterInfo.Name;
            TypeMetadata = TypeMetadataBase.EmitType( parameterInfo.ParameterType );
            ParameterAttributes = TypeMetadataBase.EmitAttributes( parameterInfo.GetCustomAttributes() );
            Kind = GetParameterKind( parameterInfo );
            Position = parameterInfo.Position;
            if ( parameterInfo.HasDefaultValue )
            {
                DefaultValue = parameterInfo.DefaultValue != null
                    ? parameterInfo.DefaultValue.ToString()
                    : String.Empty;
            }
        }

        #endregion

        #region Properties

        public override string Name { get; set; }
        public override TypeMetadataBase TypeMetadata { get; set; }
        public override int Position { get; set; }
        public override ParameterKindEnum Kind { get; set; }
        public override IEnumerable<TypeMetadataBase> ParameterAttributes { get; set; }
        public override string DefaultValue { get; set; }

        #endregion

        #region Private

        private ParameterKindEnum GetParameterKind( ParameterInfo parameterInfo )
        {
            ParameterKindEnum kind = parameterInfo.IsIn ? ParameterKindEnum.In :
                parameterInfo.IsOut ? ParameterKindEnum.Out :
                parameterInfo.IsRetval ? ParameterKindEnum.Ref : ParameterKindEnum.None;
            return kind;
        }

        #endregion
    }
}