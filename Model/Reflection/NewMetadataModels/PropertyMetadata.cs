using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
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
            TypeMetadata = TypeMetadataBase.EmitType(propertyInfo.PropertyType);
            PropertyAttributes = TypeMetadataBase.EmitAttributes(propertyInfo.GetCustomAttributes());
            Getter = MethodMetadata.EmitMethod(propertyInfo.GetGetMethod(true));
            Setter = MethodMetadata.EmitMethod(propertyInfo.GetSetMethod(true));
        }

        #endregion

        #region Properties

        public override string Name { get; set; }
        public override IEnumerable<TypeMetadataBase> PropertyAttributes { get; set; }
        public override Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public override TypeMetadataBase TypeMetadata { get; set; }
        public override PropertyInfo PropertyInfo { get; set; }
        public override MethodMetadataBase Getter { get; set; }
        public override MethodMetadataBase Setter { get; set; }

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