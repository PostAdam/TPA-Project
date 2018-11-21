using System;
using System.Collections.Generic;
using System.Linq;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
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

        #region Properties

        public override string NamespaceName { get; set; }
        public override IEnumerable<TypeMetadataBase> Types { get; set; }

        #endregion
    }
}