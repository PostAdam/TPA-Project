using System.Collections.Generic;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class NamespaceMetadataBase
    {
        public abstract string NamespaceName { get; set; }

        public abstract IEnumerable<TypeMetadataBase> Types { get; set; }
    }
}