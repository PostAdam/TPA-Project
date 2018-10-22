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
            if(parameterInfo.HasDefaultValue)
            DefaultValue = parameterInfo.DefaultValue != null ? parameterInfo.DefaultValue.ToString() : String.Empty;
        }

        #endregion

        #region Internals

        [DataMember] public string Name;
        [DataMember] public TypeMetadata TypeMetadata;
        [DataMember] public int Position;
        [DataMember] public ParameterKindEnum Kind;
        [DataMember] public IEnumerable<TypeMetadata> ParameterAttributes;
        [DataMember] public string DefaultValue;

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