using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Reflection;
using ModelBase;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class AssemblyMetadata
    {
        #region Constructor

        public AssemblyMetadata( AssemblyMetadataBase assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            Namespaces = GetNamespacesMetadata( assemblyMetadata.Namespaces );
        }

        public AssemblyMetadata()
        {
        }

        internal AssemblyMetadata( Assembly assembly )
        {
            Name = assembly.ManifestModule.Name;
            Namespaces = from Type _type in assembly.GetTypes()
                group _type by _type.GetNamespace()
                into _group
                orderby _group.Key
                select new NamespaceMetadata( _group.Key, _group );
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public IEnumerable<NamespaceMetadata> Namespaces { get; set; }

        #endregion

        public AssemblyMetadataBase GetOriginalAssemblyMetadata()
        {
            return new AssemblyMetadataBase
            {
                Name = Name,
                Namespaces = GetNameSpaces()
            };
        }

        private IEnumerable<NamespaceMetadataBase> GetNameSpaces()
        {
            List<NamespaceMetadataBase> namespaces = new List<NamespaceMetadataBase>();
            foreach ( NamespaceMetadata namespaceMetadata in Namespaces )
            {
                namespaces.Add( namespaceMetadata.GetOriginalNamespaceMetadata() );
            }

            return namespaces;
        }
    }
}