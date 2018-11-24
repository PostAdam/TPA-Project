using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
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

        public virtual AssemblyMetadata AssemblyMetadata { get; set; }

        [Key]
        public string NamespaceName { get; set; }

        public virtual IEnumerable<TypeMetadataSurrogate> Types { get; set; }

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