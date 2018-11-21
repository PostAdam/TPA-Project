using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( IsReference = true, Name = "TypeMetadata" )]
    public class TypeMetadataSurrogate
    {
        #region Constructor

        public TypeMetadataSurrogate( TypeMetadata typeMetadata )
        {
            TypeName = typeMetadata.TypeName;
            NamespaceName = typeMetadata.NamespaceName;
            BaseType = typeMetadata.BaseType;
            DeclaringType = typeMetadata.DeclaringType;
            TypeKind = typeMetadata.TypeKind;
            Modifiers = typeMetadata.Modifiers;
            Fields = typeMetadata.Fields;

            GenericArguments =  typeMetadata.GenericArguments;
            Attributes =  typeMetadata.Attributes;
            ImplementedInterfaces =  typeMetadata.ImplementedInterfaces;
            NestedTypes =  typeMetadata.NestedTypes;
            Properties =  typeMetadata.Properties;
            Methods =  typeMetadata.Methods;
            Constructors =  typeMetadata.Constructors;
            Events = typeMetadata.Events;
            FullName = typeMetadata.FullName;
        }

        #endregion

        #region Properties

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public TypeMetadata BaseType { get; set; }

        [DataMember]
        public TypeMetadata DeclaringType { get; set; }

        [DataMember]
        public TypeKind TypeKind { get; set; }

        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }

        [DataMember]
        public IEnumerable<FieldMetadata> Fields { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> Attributes { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> ImplementedInterfaces { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> NestedTypes { get; set; }

        [DataMember]
        public IEnumerable<PropertyMetadata> Properties { get; set; }

        [DataMember]
        public IEnumerable<MethodMetadata> Methods { get; set; }

        [DataMember]
        public IEnumerable<MethodMetadata> Constructors { get; set; }

        [DataMember]
        public IEnumerable<EventMetadata> Events { get; set; }

        [DataMember]
        public string FullName { get; set; }

        #endregion
    }
}