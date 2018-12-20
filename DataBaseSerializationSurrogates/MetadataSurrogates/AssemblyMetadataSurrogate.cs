using System.Collections.Generic;
using ModelBase;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class AssemblyMetadataSurrogate
    {
        #region Constructors

        public AssemblyMetadataSurrogate()
        {
        }

        public AssemblyMetadataSurrogate( AssemblyMetadataBase assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            Namespaces = GetNamespacesMetadata( assemblyMetadata.Namespaces );
        }

        #endregion

        #region Properties

        public int? AssemblyId { get; set; }
        public string Name { get; set; }
        public ICollection<NamespaceMetadataSurrogate> Namespaces { get; set; }

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