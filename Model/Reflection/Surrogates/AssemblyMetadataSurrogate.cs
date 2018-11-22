/*using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.NewMetadataModels;

namespace Model.Reflection.Surrogates
{
    [DataContract( Name = "AssemblyMetadata" )]
    public class AssemblyMetadataSurrogate
    {
        private readonly AssemblyMetadata _realAssemblyMetadata;

        #region Constructor

        public AssemblyMetadataSurrogate( AssemblyMetadata assemblyMetadata )
        {
            _realAssemblyMetadata = assemblyMetadata ?? new AssemblyMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name
        {
            get => _realAssemblyMetadata.Name;
            set => _realAssemblyMetadata.Name = value;
        }

        [DataMember]
        public IEnumerable<NamespaceMetadata> Namespaces
        {
            get => _realAssemblyMetadata.Namespaces;
            set => _realAssemblyMetadata.Namespaces = value;
        }

        #endregion

        public AssemblyMetadata GetOriginalAssemblyMetadata()
        {
            return _realAssemblyMetadata;
        }
    }
}*/