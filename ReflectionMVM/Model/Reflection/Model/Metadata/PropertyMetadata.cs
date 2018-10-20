using System;
using System.Collections;
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

        private PropertyMetadata(string propertyName, TypeMetadata propertyType, IEnumerable<AttributeMetadata> attributes)
        {
            Name = propertyName;
            Modifiers = propertyType.Modifiers;
            TypeMetadata = propertyType;
            PropertyAttributes = attributes;
        }

        #endregion

        #region Internals

        internal static IEnumerable<PropertyMetadata> EmitProperties( IEnumerable<PropertyInfo> props )
        {
            return from prop in props
                where prop.GetGetMethod().GetVisible() || prop.GetSetMethod().GetVisible()
                select new PropertyMetadata( prop.Name, TypeMetadata.EmitReference( prop.PropertyType ), TypeMetadata.EmitAttributes(prop.GetCustomAttributes()) );
        }

        [DataMember] internal string Name;
        [DataMember] internal IEnumerable<AttributeMetadata> PropertyAttributes;
        [DataMember] internal Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;
        [DataMember] internal TypeMetadata TypeMetadata;

        #endregion
    }
}