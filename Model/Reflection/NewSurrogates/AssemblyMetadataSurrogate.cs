using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;
using static Model.Reflection.NewSurrogates.CollectionTypeAccessor;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( Name = "AssemblyMetadata" )]
    public class AssemblyMetadataSurrogate
    {
        #region Constructor

        public AssemblyMetadataSurrogate( AssemblyMetadata assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            Namespaces = GetNamespacesMetadata( assemblyMetadata.Namespaces );
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