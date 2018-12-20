using System.Collections.Generic;
using ModelBase;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class AssemblyMetadataSurrogate
    {
        #region Constructor

        public AssemblyMetadataSurrogate( AssemblyMetadataBase assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            Namespaces = GetNamespacesMetadata( assemblyMetadata.Namespaces );
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public IEnumerable<NamespaceMetadataSurrogate> Namespaces { get; set; }

        #endregion

        public AssemblyMetadataBase GetOriginalAssemblyMetadata()
        {
            return new AssemblyMetadataBase
            {
                Name = Name,
                Namespaces = GetNameSpaces()
            };
        }

        private IEnumerable<NamespaceMetadataBase> GetNameSpaces()
        {
            List<NamespaceMetadataBase> namespaces = new List<NamespaceMetadataBase>();
            foreach ( NamespaceMetadataSurrogate namespaceMetadata in Namespaces )
            {
                namespaces.Add( namespaceMetadata.GetOriginalNamespaceMetadata() );
            }

            return namespaces;
        }
    }
}