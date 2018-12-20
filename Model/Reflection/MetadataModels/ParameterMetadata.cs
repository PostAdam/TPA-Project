using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;
using ModelBase;

namespace Model.Reflection.MetadataModels
{
    public class ParameterMetadata : ParameterMetadataBase
    {
        #region Constructors

        public ParameterMetadata()
        {
        }

        internal ParameterMetadata(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name;
            TypeMetadata = MetadataModels.TypeMetadata.EmitType(parameterInfo.ParameterType);
            ParameterAttributes = MetadataModels.TypeMetadata.EmitAttributes(parameterInfo.GetCustomAttributes());
            Kind = GetParameterKind(parameterInfo);
            Position = parameterInfo.Position;
            if (parameterInfo.HasDefaultValue)
            {
                DefaultValue = parameterInfo.DefaultValue != null
                    ? parameterInfo.DefaultValue.ToString()
                    : String.Empty;
            }
        }

        #endregion

        #region Private

        private ParameterKindEnum GetParameterKind(ParameterInfo parameterInfo)
        {
            ParameterKindEnum kind = parameterInfo.IsIn ? ParameterKindEnum.In :
                parameterInfo.IsOut ? ParameterKindEnum.Out :
                parameterInfo.IsRetval ? ParameterKindEnum.Ref : ParameterKindEnum.None;
            return kind;
        }

        #endregion
    }
}