using System.Collections.Generic;
using Model.Reflection.MetadataModels;
using ModelBase;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class NamespaceMetadataSurrogate
    {
        #region Constructors

        public NamespaceMetadataSurrogate()
        {
        }

        public NamespaceMetadataSurrogate( NamespaceMetadataBase namespaceMetadata )
        {
            NamespaceName = namespaceMetadata.NamespaceName;
            Types = GetTypesMetadata( namespaceMetadata.Types );
        }

        #endregion

        #region Properties

        public int? NamespaceId { get; set; }
        public string NamespaceName { get; set; }
        public ICollection<TypeMetadataSurrogate> Types { get; set; }

        #endregion

        #region Navigation Properties

        public int? AssemblyForeignId { get; set; } 
        public AssemblyMetadataSurrogate AssemblySurrogate { get; set; } 

        #endregion

        public NamespaceMetadataBase GetOriginalNamespaceMetadata()
        {
            return new NamespaceMetadataBase
            {
                NamespaceName = NamespaceName,
                Types = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( Types )
            };
        }

        private ICollection<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            ICollection<TypeMetadataSurrogate> surrogates = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadataBase typeMetadata in types )
            {
                surrogates.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return surrogates;
        }
    }
}