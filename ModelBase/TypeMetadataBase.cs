using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;

namespace ModelBase
{
    public class TypeMetadataBase
    {
        #region Properties

        public string TypeName { get; set; }
        public string NamespaceName { get; set; }
        public TypeMetadataBase BaseType { get; set; }
        public TypeMetadataBase DeclaringType { get; set; }
        public TypeKind TypeKind { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
        public IEnumerable<FieldMetadataBase> Fields { get; set; }
        public IEnumerable<TypeMetadataBase> GenericArguments { get; set; }
        public IEnumerable<TypeMetadataBase> Attributes { get; set; }
        public IEnumerable<TypeMetadataBase> ImplementedInterfaces { get; set; }
        public IEnumerable<TypeMetadataBase> NestedTypes { get; set; }
        public IEnumerable<PropertyMetadataBase> Properties { get; set; }
        public IEnumerable<MethodMetadataBase> Methods { get; set; }
        public IEnumerable<MethodMetadataBase> Constructors { get; set; }
        public IEnumerable<EventMetadataBase> Events { get; set; }
        public string FullName { get; set; }

        public const BindingFlags AllAccessLevels = BindingFlags.NonPublic
                                             | BindingFlags.DeclaredOnly
                                             | BindingFlags.Public
                                             | BindingFlags.Static
                                             | BindingFlags.Instance;

        #endregion
    }
}