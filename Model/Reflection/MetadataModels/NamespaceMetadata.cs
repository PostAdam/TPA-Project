using System;
using System.Collections.Generic;
using System.Linq;
using ModelBase;

namespace Model.Reflection.MetadataModels
{
    public class NamespaceMetadata
    {
        #region Constructors

        public NamespaceMetadata()
        {
        }

        public NamespaceMetadata( string name, IEnumerable<Type> types )
        {
            NamespaceName = name;
            Types = from type in types
                orderby type.Name
                select new TypeMetadata(type);
        }

        #endregion

        #region Properties

        public string NamespaceName { get; set; }
        public IEnumerable<TypeMetadata> Types { get; set; }
        public NamespaceMetadataBase NamespaceMetadataBase { get; set; }

        #endregion
    }
}