using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( Name = "AssemblyMetadata" )]
    public class AssemblyMetadataSurrogate
    {
        #region Constructor

        public AssemblyMetadataSurrogate( AssemblyMetadata assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            Namespaces =  assemblyMetadata.Namespaces;
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public IEnumerable<NamespaceMetadata> Namespaces { get; set; }

        #endregion
    }
}