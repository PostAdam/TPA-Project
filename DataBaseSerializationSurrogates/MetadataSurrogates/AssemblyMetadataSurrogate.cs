using System.Collections.Generic;
using Model.Reflection.MetadataModels;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class AssemblyMetadataSurrogate
    {
        #region Constructors

        public AssemblyMetadataSurrogate()
        {
        }

        public AssemblyMetadataSurrogate( AssemblyMetadata assemblyMetadata )
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