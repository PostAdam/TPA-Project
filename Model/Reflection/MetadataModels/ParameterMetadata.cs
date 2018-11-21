using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
{
    public class ParameterMetadata
    {
        #region Constructors

        public ParameterMetadata()
        {
        }

        internal ParameterMetadata(ParameterInfo parameterInfo)
        {
            Name = parameterInfo.Name;
            TypeMetadata = TypeMetadata.EmitType(parameterInfo.ParameterType);
            ParameterAttributes = TypeMetadata.EmitAttributes(parameterInfo.GetCustomAttributes());
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

        #region Properties

        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
        public int Position { get; set; }
        public ParameterKindEnum Kind { get; set; }
        public IEnumerable<TypeMetadata> ParameterAttributes { get; set; }
        public string DefaultValue { get; set; }

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