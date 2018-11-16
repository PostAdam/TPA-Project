using System.Collections.Generic;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class AssemblyMetadataBase
    {
        public abstract string Name { get; set; }
        public abstract IEnumerable<NamespaceMetadataBase> Namespaces { get; set; }
    }
}