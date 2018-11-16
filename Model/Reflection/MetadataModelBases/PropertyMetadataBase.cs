using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class PropertyMetadataBase
    {
        public abstract string Name { get; set; }
        public abstract IEnumerable<TypeMetadataBase> PropertyAttributes { get; set; }
        public abstract Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public abstract TypeMetadataBase TypeMetadata { get; set; }
        public abstract PropertyInfo PropertyInfo { get; set; }
        public abstract MethodMetadataBase Getter { get; set; }
        public abstract MethodMetadataBase Setter { get; set; }
    }
}