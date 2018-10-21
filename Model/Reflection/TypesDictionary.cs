using System.Collections.Generic;
using Model.Reflection.MetadataModels;

namespace Model.Reflection
{
    public static class TypesDictionary
    {
        public static Dictionary<string, TypeMetadata> ReflectedTypes { get; } =
            new Dictionary<string, TypeMetadata>();
    }
}