using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract( IsReference = true )]
    internal class PropertyMetadata
    {
        #region Constructor

        private PropertyMetadata(string propertyName, TypeMetadata propertyType)
        {
            Name = propertyName;
            TypeMetadata = propertyType;
        }

        #endregion

        #region Internals

        internal static IEnumerable<PropertyMetadata> EmitProperties( IEnumerable<PropertyInfo> props )
        {
            return from prop in props
                where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                select new PropertyMetadata( prop.Name, TypeMetadata.EmitReference( prop.PropertyType ) );
        }

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;

        #endregion
    }
}