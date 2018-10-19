using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract( IsReference = true )]
    public class NamespaceMetadata
    {
        #region Constructor

        public NamespaceMetadata( string name, IEnumerable<Type> types )
        {
            NamespaceName = name;
            Types = from type in types
                orderby type.Name
                select new TypeMetadata( type );
        }

        #endregion

        #region Internals

        [DataMember]
        internal string NamespaceName { get; set; }

        [DataMember]
        internal IEnumerable<TypeMetadata> Types { get; set; }

        #endregion
    }
}