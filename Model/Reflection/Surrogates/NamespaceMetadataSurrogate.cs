using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.Surrogates
{
    [DataContract( IsReference = true )]
    public class NamespaceMetadataSurrogate : NamespaceMetadataBase
    {
        private readonly NamespaceMetadataBase _realNamespaceMetadata;// TODO: change to NamespaceMetadata type

        #region Constructor

        public NamespaceMetadataSurrogate( NamespaceMetadataBase realNamespaceMetadata )
        {
            //            _realNamespaceMetadata = realNamespaceMetadata; // TODO: need to change NamespaceMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public override string NamespaceName
        {
            get => _realNamespaceMetadata.NamespaceName;
            set => _realNamespaceMetadata.NamespaceName = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> Types
        {
            get => _realNamespaceMetadata.Types;
            set => _realNamespaceMetadata.Types = value;
        }

        #endregion
    }
}