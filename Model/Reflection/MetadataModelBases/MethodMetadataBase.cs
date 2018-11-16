using System;
using System.Collections.Generic;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class MethodMetadataBase
    {
        public abstract string Name { get; set; }
        public abstract bool Extension { get; set; }
        public abstract TypeMetadataBase ReturnType { get; set; }
        public abstract IEnumerable<TypeMetadataBase> MethodAttributes { get; set; }
        public abstract IEnumerable<ParameterMetadataBase> Parameters { get; set; }
        public abstract IEnumerable<TypeMetadataBase> GenericArguments { get; set; }
        public abstract Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
    }
}