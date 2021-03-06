﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.MetadataModels;
using ModelBase;
using Model.Reflection.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class PropertyMetadata
    {
        #region Constructor
        public PropertyMetadata()
        {
        }

        internal PropertyMetadata( PropertyInfo propertyInfo )
        {
            Name = propertyInfo.Name;
            Getter = MethodMetadata.EmitMethod(propertyInfo.GetGetMethod(true));
            Setter = MethodMetadata.EmitMethod(propertyInfo.GetSetMethod(true));
            Modifiers = PropertyReflector.GetModifier(Getter ,Setter);
            TypeMetadata = TypeReflector.EmitType( propertyInfo.PropertyType );
            PropertyAttributes = TypeReflector.EmitAttributes( propertyInfo.CustomAttributes );
        }

        public PropertyMetadata( PropertyMetadataBase propertyMetadata )
        {
            Name = propertyMetadata.Name;
            PropertyAttributes = GetTypesMetadata( propertyMetadata.PropertyAttributes );
            Modifiers = propertyMetadata?.Modifiers != null ? Tuple.Create((Model.Reflection.Enums.AccessLevel) propertyMetadata.Modifiers.Item1, (Model.Reflection.Enums.AbstractEnum) propertyMetadata.Modifiers.Item2,
                (Model.Reflection.Enums.StaticEnum) propertyMetadata.Modifiers.Item3, (Model.Reflection.Enums.VirtualEnum) propertyMetadata.Modifiers.Item4 ) : null;
            TypeMetadata = TypeMetadata.EmitSurrogateTypeMetadata( propertyMetadata.TypeMetadata );
            Getter = propertyMetadata.Getter == null ? null : new MethodMetadata( propertyMetadata.Getter );
            Setter = propertyMetadata.Setter == null ? null : new MethodMetadata( propertyMetadata.Setter );
        }

        #endregion

        #region Properties


        public string Name { get; set; }
        public IEnumerable<TypeMetadata> PropertyAttributes { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
        public MethodMetadata Getter { get; set; }
        public MethodMetadata Setter { get; set; }

        #endregion

        public PropertyMetadataBase GetOriginalPropertyMetadata()
        {

            return new PropertyMetadataBase()
            {
                Name = Name,
                PropertyAttributes = GetOriginalTypesMetadata( PropertyAttributes ),
                Modifiers = Modifiers != null ? Tuple.Create( ( ModelBase.Enums.AccessLevel ) Modifiers.Item1, ( ModelBase.Enums.AbstractEnum ) Modifiers.Item2,
                    ( ModelBase.Enums.StaticEnum ) Modifiers.Item3, ( ModelBase.Enums.VirtualEnum ) Modifiers.Item4 ) : null,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Getter = Getter?.GetOriginalMethodMetadata(),
                Setter = Setter?.GetOriginalMethodMetadata()
            };
        }
    }
}