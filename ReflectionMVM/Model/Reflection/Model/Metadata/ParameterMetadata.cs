using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
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

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;
        [DataMember] internal int Position;
        [DataMember] internal IEnumerable<TypeMetadata> ParameterAttributes;
        [DataMember] internal object DefaultValue;

        #endregion
    }
}