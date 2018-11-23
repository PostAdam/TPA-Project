using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( IsReference = true, Name = "NamespaceMetadata" )]
    public class NamespaceMetadataSurrogate
    {
        #region Constructor

        public NamespaceMetadataSurrogate( NamespaceMetadata namespaceMetadata )
        {
            NamespaceName = namespaceMetadata.NamespaceName;
            Types = GetTypesMetadata( namespaceMetadata.Types );
        }

        #endregion

        #region Properties

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> Types { get; set; }

        #endregion

        public NamespaceMetadata GetOriginalNamespaceMetadata()
        {
            return new NamespaceMetadata
            {
                NamespaceName = NamespaceName,
                Types = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( Types )
            };
        }

        private IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            List<TypeMetadataSurrogate> typeMetadatas = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                typeMetadatas.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return typeMetadatas;
        }
    }
}