using System;
using System.Collections.Generic;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class FieldMetadataBase
    {
        public abstract string Name { get; set; }
        public abstract TypeMetadataBase TypeMetadata { get; set; }
        public abstract Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public abstract StaticEnum IsStatic { get; set; }
        public abstract IEnumerable<TypeMetadataBase> FieldAttributes { get; set; }
    }
}