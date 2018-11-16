using System;
using System.Collections.Generic;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class TypeMetadataBase
    {
        public abstract string TypeName { get; internal set; }
        public abstract string NamespaceName { get; internal set; }
        public abstract TypeMetadataBase BaseType { get; internal set; }
        public abstract TypeMetadataBase DeclaringType { get; internal set; }
        public abstract TypeKind TypeKind { get; internal set; }
        public abstract Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; internal set; }
        public abstract IEnumerable<FieldMetadataBase> Fields { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> GenericArguments { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> Attributes { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> ImplementedInterfaces { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> NestedTypes { get; internal set; }
        public abstract IEnumerable<PropertyMetadataBase> Properties { get; internal set; }
        public abstract IEnumerable<MethodMetadataBase> Methods { get; internal set; }
        public abstract IEnumerable<MethodMetadataBase> Constructors { get; internal set; }
        public abstract IEnumerable<EventMetadataBase> Events { get; internal set; }
        public abstract string FullName { get; internal set; }
    }
}