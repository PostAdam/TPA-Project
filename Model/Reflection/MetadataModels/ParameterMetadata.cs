using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model.Reflection.MetadataModels
{
    [DataContract(IsReference = true)]
    public class ParameterMetadata
    {
        #region Constructor

        internal ParameterMetadata(ParameterInfo parameterInfo)
        {
//TODO: get more data from reflection and change viewmodel (like default value, is out, is ref etc.)
            Name = parameterInfo.Name;
            TypeMetadata = TypeMetadata.EmitType(parameterInfo.ParameterType);
            ParameterAttributes = TypeMetadata.EmitAttributes(parameterInfo.GetCustomAttributes());
        }

        #endregion

        #region Internals

        [DataMember] public string Name;
        [DataMember] public TypeMetadata TypeMetadata;
        [DataMember] public int Position;
        [DataMember] public IEnumerable<TypeMetadata> ParameterAttributes;
        [DataMember] public object DefaultValue;

        #endregion
    }
}