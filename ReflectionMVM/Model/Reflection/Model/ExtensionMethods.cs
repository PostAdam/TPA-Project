using System;
using System.Reflection;

namespace Project.Model.Reflection.Model
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