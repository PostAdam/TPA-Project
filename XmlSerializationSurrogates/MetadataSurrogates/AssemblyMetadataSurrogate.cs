using System.Collections.Generic;
using System.Runtime.Serialization;
using ModelBase;
using static XmlSerializationSurrogates.CollectionTypeAccessor;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( Name = "AssemblyMetadata" )]
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

        [DataMember]
        public string Name { get; set; }

        [DataMember]
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