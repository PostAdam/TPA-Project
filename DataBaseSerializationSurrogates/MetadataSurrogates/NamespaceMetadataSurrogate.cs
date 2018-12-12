using System.Collections.Generic;
using System.Linq;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class NamespaceMetadataSurrogate
    {
        #region Constructors

        public NamespaceMetadataSurrogate()
        {
        }

        public NamespaceMetadataSurrogate( NamespaceMetadata namespaceMetadata )
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

        public NamespaceMetadata GetOriginalNamespaceMetadata()
        {
            return new NamespaceMetadata
            {
                NamespaceName = NamespaceName,
                Types = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( Types )
            };
        }

        private ICollection<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            //            return types.Select( TypeMetadataSurrogate.EmitSurrogateTypeMetadata );
            List<TypeMetadataSurrogate> surrogates = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                surrogates.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return surrogates;
        }
    }
}