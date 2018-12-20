using System;
using System.Reflection;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
{
    public class FieldReflector
    {
        internal static Tuple<AccessLevel, StaticEnum> GetModifier( FieldInfo fieldInfo )
        {
            AccessLevel access = AccessLevel.Private;
            if ( fieldInfo.IsPublic )
                access = AccessLevel.Public;
            else if ( fieldInfo.IsFamilyOrAssembly )
                access = AccessLevel.Public;
            else if ( fieldInfo.IsFamily )
                access = AccessLevel.Protected;
            else if ( fieldInfo.IsAssembly )
                access = AccessLevel.Internal;

            StaticEnum _static = StaticEnum.NotStatic;
            if ( fieldInfo.IsStatic )
                _static = StaticEnum.Static;

            return new Tuple<AccessLevel, StaticEnum>( access, _static );
        }
    }
}