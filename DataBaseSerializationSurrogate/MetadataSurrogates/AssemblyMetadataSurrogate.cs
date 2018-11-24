using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogate.MetadataSurrogates
{
    public class AssemblyMetadataSurrogate
    {
        #region Constructor

        public AssemblyMetadataSurrogate( AssemblyMetadata assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            Namespaces = CollectionTypeAccessor.GetNamespacesMetadata( assemblyMetadata.Namespaces );
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public virtual IEnumerable<NamespaceMetadataSurrogate> Namespaces { get; set; }

        #endregion

        public AssemblyMetadata GetOriginalAssemblyMetadata()
        {
            return new AssemblyMetadata
            {
                Name = Name,
                Namespaces = GetNameSpaces()
            };
        }

        private IEnumerable<NamespaceMetadata> GetNameSpaces()
        {
            List<NamespaceMetadata> namespaces = new List<NamespaceMetadata>();
            foreach ( NamespaceMetadataSurrogate namespaceMetadata in Namespaces )
            {
                namespaces.Add( namespaceMetadata.GetOriginalNamespaceMetadata() );
            }

            return namespaces;
        }
    }
}