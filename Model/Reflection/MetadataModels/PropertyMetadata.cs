using System;
using System.Collections.Generic;
using System.Reflection;
using ModelBase;
using ModelBase.Enums;
using AbstractEnum = Model.Reflection.Enums.AbstractEnum;
using AccessLevel = Model.Reflection.Enums.AccessLevel;
using StaticEnum = Model.Reflection.Enums.StaticEnum;
using VirtualEnum = Model.Reflection.Enums.VirtualEnum;

namespace Model.Reflection.MetadataModels
{
    public class PropertyMetadata
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

        #region Properties

        public string Name { get; set; }
        public IEnumerable<TypeMetadata> PropertyAttributes { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
        public MethodMetadata Getter { get; set; }
        public MethodMetadata Setter { get; set; }
        public PropertyMetadataBase PropertyMetadataBase { get; set; }

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