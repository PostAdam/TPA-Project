using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( IsReference = true, Name = "NamespaceMetadata" )]
    public class NamespaceMetadataSurrogate
    {
        #region Constructor

        public NamespaceMetadataSurrogate( NamespaceMetadata namespaceMetadata )
        {
            NamespaceName = namespaceMetadata.NamespaceName;
            Types = namespaceMetadata.Types;
        }

        #endregion

        #region Properties

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> Types { get; set; }

        #endregion
    }
}