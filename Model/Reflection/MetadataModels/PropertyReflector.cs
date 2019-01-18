using System;
using Model.ModelDTG;
using Model.Reflection.Enums;


namespace Model.Reflection.MetadataModels
{
    public class PropertyReflector
    {
        internal static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> GetModifier(MethodMetadata getter,
            MethodMetadata setter)
        {
            if (getter == null && setter == null) return null;

            if (getter == null)
            {
                return setter.Modifiers;
            }

            if (setter == null)
            {
                return getter.Modifiers;
            }

            return getter.Modifiers.Item1 < setter.Modifiers.Item1 ? getter.Modifiers : setter.Modifiers;
        }
    }
}