using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( Name = "AssemblyMetadata" )]
    public class AssemblyMetadataSurrogate
    {
        #region Constructor

        public AssemblyMetadataSurrogate( AssemblyMetadata assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            Namespaces = CollectionTypeAccessor.GetNamespacesMetadata( assemblyMetadata.Namespaces.ToList() );
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<NamespaceMetadataSurrogate> Namespaces { get; set; }

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
                namespaces.Add( namespaceMetadata.GetOryginalNamespaceMetadata() );
            }

            return namespaces;
        }
    }
}