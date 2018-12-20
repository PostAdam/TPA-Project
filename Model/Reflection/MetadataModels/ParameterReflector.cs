using System.Reflection;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
{
    public class ParameterReflector
    {

        internal static ParameterKindEnum GetParameterKind(ParameterInfo parameterInfo)
        {
            ParameterKindEnum kind = parameterInfo.IsIn ? ParameterKindEnum.In :
                parameterInfo.IsOut ? ParameterKindEnum.Out :
                parameterInfo.IsRetval ? ParameterKindEnum.Ref : ParameterKindEnum.None;
            return kind;
        }

    }
}