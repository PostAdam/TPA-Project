using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
{
    public class AssemblyMetadata : AssemblyMetadataBase
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

        public override string Name { get; set; }
        public override IEnumerable<NamespaceMetadataBase> Namespaces { get; set; }

        #endregion
    }
}