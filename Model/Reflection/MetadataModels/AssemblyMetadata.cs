using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model.Reflection.MetadataModels
{
    [DataContract]
    public class AssemblyMetadata
    {
        #region Constructor

        public AssemblyMetadata()
        {
        }

        internal AssemblyMetadata(Assembly assembly)
        {
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                group _type by _type.GetNamespace()
                into _group
                orderby _group.Key
                select new NamespaceMetadata(_group.Key, _group);
        }

        #endregion

        #region Properties

        [DataMember] public string Name { get; set; }

        [DataMember] public IEnumerable<NamespaceMetadata> Namespaces { get; set; }

        #endregion
    }
}