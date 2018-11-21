using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    [DataContract( IsReference = true, Name = "TypeMetadata" )]
    public class TypeMetadataSurrogate
    {
        private readonly TypeMetadata _realMetadata;

        #region Constructor

        public TypeMetadataSurrogate()
        {
            _realMetadata = new TypeMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string TypeName
        {
            get => _realMetadata.TypeName;
            internal set => _realMetadata.TypeName = value;
        }
        [DataMember]
        public string NamespaceName
        {
            get => _realMetadata.NamespaceName;
            internal set => _realMetadata.NamespaceName = value;
        }
        [DataMember]
        public TypeMetadata BaseType
        {
            get => _realMetadata.BaseType;
            internal set => _realMetadata.BaseType = value;
        }
        [DataMember]
        public TypeMetadata DeclaringType
        {
            get => _realMetadata.DeclaringType;
            internal set => _realMetadata.DeclaringType = value;
        }
        [DataMember]
        public TypeKind TypeKind
        {
            get => _realMetadata.TypeKind;
            internal set => _realMetadata.TypeKind = value;
        }
        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers
        {
            get => _realMetadata.Modifiers;
            internal set => _realMetadata.Modifiers = value;
        }
        [DataMember]
        public IEnumerable<FieldMetadata> Fields
        {
            get => _realMetadata.Fields;
            internal set => _realMetadata.Fields = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> GenericArguments
        {
            get => _realMetadata.GenericArguments;
            internal set => _realMetadata.GenericArguments = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> Attributes
        {
            get => _realMetadata.Attributes;
            internal set => _realMetadata.Attributes = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> ImplementedInterfaces
        {
            get => _realMetadata.ImplementedInterfaces;
            internal set => _realMetadata.ImplementedInterfaces = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> NestedTypes
        {
            get => _realMetadata.NestedTypes;
            internal set => _realMetadata.NestedTypes = value;
        }
        [DataMember]
        public IEnumerable<PropertyMetadata> Properties
        {
            get => _realMetadata.Properties;
            internal set => _realMetadata.Properties = value;
        }
        [DataMember]
        public IEnumerable<MethodMetadata> Methods
        {
            get => _realMetadata.Methods;
            internal set => _realMetadata.Methods = value;
        }
        [DataMember]
        public IEnumerable<MethodMetadata> Constructors
        {
            get => _realMetadata.Constructors;
            internal set => _realMetadata.Constructors = value;
        }
        [DataMember]
        public IEnumerable<EventMetadata> Events
        {
            get => _realMetadata.Events;
            internal set => _realMetadata.Events = value;
        }
        [DataMember]
        public string FullName
        {
            get => _realMetadata.FullName;
            internal set => _realMetadata.FullName = value;
        }

        #endregion
    }
}