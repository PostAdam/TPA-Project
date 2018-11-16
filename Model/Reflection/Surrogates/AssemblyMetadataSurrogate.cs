using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.Surrogates
{
    [DataContract]
    public class AssemblyMetadataSurrogate : AssemblyMetadataBase
    {
        private readonly AssemblyMetadataBase _realAssemblyMetadata;// TODO: change to AssemblyMetadata type

        #region Constructor

        public AssemblyMetadataSurrogate( AssemblyMetadataBase realAssemblyMetadata )
        {
            //            _realAssemblyMetadata = realAssemblyMetadata; // TODO: need to change AssemblyMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public int Id { get; set; } // TODO: not sure if it should only be in surrogate type
        [DataMember]
        public override string Name
        {
            get => _realAssemblyMetadata.Name;
            set => _realAssemblyMetadata.Name = value;
        }
        [DataMember]
        public override IEnumerable<NamespaceMetadataBase> Namespaces
        {
            get => _realAssemblyMetadata.Namespaces;
            set => _realAssemblyMetadata.Namespaces = value;
        }

        #endregion
    }
}