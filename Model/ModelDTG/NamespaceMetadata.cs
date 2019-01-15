using System;
using System.Collections.Generic;
using System.Linq;
using ModelBase;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;

namespace Model.ModelDTG
{
    public class NamespaceMetadata
    {
        #region Constructor
        public NamespaceMetadata()
        {
        }

        public NamespaceMetadata( string name, IEnumerable<Type> types )
        {
            NamespaceName = name;
            Types = from type in types
                orderby type.Name
                select new TypeMetadata( type );
        }

        public NamespaceMetadata( NamespaceMetadataBase namespaceMetadata )
        {
            NamespaceName = namespaceMetadata.NamespaceName;
            Types = GetTypesMetadata( namespaceMetadata.Types );
        }

        #endregion

        #region Properties

        public string NamespaceName { get; set; }
        public IEnumerable<TypeMetadata> Types { get; set; }

        #endregion

        public NamespaceMetadataBase GetOriginalNamespaceMetadata()
        {
            return new NamespaceMetadataBase
            {
                NamespaceName = NamespaceName,
                Types = GetOriginalTypesMetadata( Types )
            };
        }

        private IEnumerable<TypeMetadata> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            List<TypeMetadata> typeMetadatas = new List<TypeMetadata>();
            foreach ( TypeMetadataBase typeMetadata in types )
            {
                typeMetadatas.Add( TypeMetadata.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return typeMetadatas;
        }
    }
}