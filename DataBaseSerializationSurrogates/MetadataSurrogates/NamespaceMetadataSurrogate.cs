using System.Collections.Generic;
using System.Linq;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class NamespaceMetadataSurrogate
    {
        #region Constructor

        public NamespaceMetadataSurrogate( NamespaceMetadata namespaceMetadata )
        {
            NamespaceName = namespaceMetadata.NamespaceName;
            Types = GetTypesMetadata( namespaceMetadata.Types ) as ICollection<TypeMetadataSurrogate>;
        }

        #endregion

        #region Properties

        public int NamespaceId { get; set; }
        public string NamespaceName { get; set; }
        public ICollection<TypeMetadataSurrogate> Types { get; set; }

        #endregion

        #region Navigation Properties

        public AssemblyMetadataSurrogate AssemblyMetadataSurrogate { get; set; } 

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
            return types.Select( TypeMetadataSurrogate.EmitSurrogateTypeMetadata );
        }
    }
}