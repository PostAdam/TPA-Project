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
            Modifiers = GetModifier();
            TypeMetadata = TypeMetadata.EmitType(propertyInfo.PropertyType);
            PropertyAttributes = TypeMetadata.EmitAttributes(propertyInfo.GetCustomAttributes());
            Getter = MethodMetadata.EmitMethod(propertyInfo.GetGetMethod(true));
            Setter = MethodMetadata.EmitMethod(propertyInfo.GetSetMethod(true));
        }

        #endregion

        #region Internals

        [DataMember] public string Name;
        [DataMember] public IEnumerable<TypeMetadata> PropertyAttributes;
        [DataMember] public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers;
        [DataMember] public TypeMetadata TypeMetadata;
        [DataMember] public PropertyInfo PropertyInfo;
        [DataMember] public MethodMetadata Getter;
        [DataMember] public MethodMetadata Setter;

        #endregion

        #region Private

        private Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> GetModifier()
        {
            if (Getter == null && Setter == null) return null;

            if (Getter == null)
            {
                return Setter.Modifiers;
            }

            if (Setter == null)
            {
                return Getter.Modifiers;
            }

            return Getter.Modifiers.Item1 < Setter.Modifiers.Item1 ? Getter.Modifiers : Setter.Modifiers;

        }
        
        #endregion
    }
}