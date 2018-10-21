using System;

namespace Model.Reflection
{
    internal static class ExtensionMethods
    {
        internal static string GetNamespace( this Type type )
        {
            string ns = type.Namespace;
            return ns ?? string.Empty;
        }
    }
}