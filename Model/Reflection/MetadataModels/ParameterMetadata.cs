using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        #region Constructor

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

        [DataMember] public string Name { get; set; }
        [DataMember] public TypeMetadata TypeMetadata { get; set; }
        [DataMember] public int Position { get; set; }
        [DataMember] public ParameterKindEnum Kind { get; set; }
        [DataMember] public IEnumerable<TypeMetadata> ParameterAttributes { get; set; }
        [DataMember] public string DefaultValue { get; set; }

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