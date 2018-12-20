using System.Collections.Generic;
using ModelBase;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;

namespace Model.ModelDTG
{
    public class NamespaceMetadataSurrogate
    {
        #region Constructor

        public NamespaceMetadataSurrogate( NamespaceMetadataBase namespaceMetadata )
        {
            NamespaceName = namespaceMetadata.NamespaceName;
            Types = GetTypesMetadata( namespaceMetadata.Types );
        }

        #endregion

        #region Properties

        public string NamespaceName { get; set; }
        public IEnumerable<TypeMetadataSurrogate> Types { get; set; }

        #endregion

        public NamespaceMetadataBase GetOriginalNamespaceMetadata()
        {
            return new NamespaceMetadataBase
            {
                NamespaceName = NamespaceName,
                Types = GetOriginalTypesMetadata( Types )
            };
        }

        private IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            List<TypeMetadataSurrogate> typeMetadatas = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadataBase typeMetadata in types )
            {
                typeMetadatas.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return typeMetadatas;
        }
    }
}