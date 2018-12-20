using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;
using ModelBase;

namespace Model.Reflection.MetadataModels
{
    public class PropertyMetadata : PropertyMetadataBase
    {
        #region Constructors

        public PropertyMetadata()
        {
        }

        internal PropertyMetadata(PropertyInfo propertyInfo)
        {
            Modifiers = GetModifier();
            TypeMetadata = MetadataModels.TypeMetadata.EmitType(propertyInfo.PropertyType);
            PropertyAttributes = MetadataModels.TypeMetadata.EmitAttributes(propertyInfo.GetCustomAttributes());
            Getter = MethodMetadata.EmitMethod(propertyInfo.GetGetMethod(true));
            Setter = MethodMetadata.EmitMethod(propertyInfo.GetSetMethod(true));
        }

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