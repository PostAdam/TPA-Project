using System;
using System.Collections.Generic;
using System.Linq;
using ModelBase;

namespace Model.Reflection.MetadataModels
{
    public class NamespaceMetadata : NamespaceMetadataBase
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

    }
}