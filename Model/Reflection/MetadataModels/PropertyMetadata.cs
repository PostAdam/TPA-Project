using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
{
    [DataContract(IsReference = true)]
    public class PropertyMetadata
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

        [DataMember] public string Name;
        [DataMember] public IEnumerable<TypeMetadata> PropertyAttributes;
        [DataMember] public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;
        [DataMember] public TypeMetadata TypeMetadata;

        #endregion
    }
}