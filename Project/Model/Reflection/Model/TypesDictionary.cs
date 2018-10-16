using System.Collections.Generic;

namespace Project.Model.Reflection.Model
{
    internal static class TypesDictionary
    {
        internal static Dictionary<string, TypeMetadata> ReflectedTypes { get; set; } =
            new Dictionary<string, TypeMetadata>();
    }
}