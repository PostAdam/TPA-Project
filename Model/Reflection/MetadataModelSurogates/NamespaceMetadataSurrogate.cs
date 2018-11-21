using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    [DataContract( IsReference = true, Name = "NamespaceMetadata" )]
    public class NamespaceMetadataSurrogate
    {
        private readonly NamespaceMetadata _realNamespaceMetadata;

        #region Constructor

        public NamespaceMetadataSurrogate( NamespaceMetadata namespaceMetadata = null )
        {
            _realNamespaceMetadata = namespaceMetadata ?? new NamespaceMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string NamespaceName
        {
            get => _realNamespaceMetadata.NamespaceName;
            set => _realNamespaceMetadata.NamespaceName = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> Types
        {
            get => _realNamespaceMetadata.Types;
            set => _realNamespaceMetadata.Types = value;
        }

        #endregion
    }
}