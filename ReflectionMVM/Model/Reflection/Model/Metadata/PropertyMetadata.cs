using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract(IsReference = true)]
    internal class PropertyMetadata
    {
        #region Constructor

        internal PropertyMetadata(PropertyInfo propertyInfo)
        {
            Name = propertyInfo.Name;
            //TODO: add getters and setter connected to this method and check their modifiers 
//            Modifiers = GetModifier(propertyInfo);
            TypeMetadata = TypeMetadata.EmitType(propertyInfo.PropertyType);
            PropertyAttributes = TypeMetadata.EmitAttributes(propertyInfo.GetCustomAttributes());
        }

        #endregion

        #region Internals

    
        [DataMember] internal string Name;
        [DataMember] internal IEnumerable<TypeMetadata> PropertyAttributes;
        [DataMember] internal Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;
        [DataMember] internal TypeMetadata TypeMetadata;
        #endregion
    }
}