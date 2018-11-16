using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.Surrogates
{
    [DataContract( IsReference = true )]
    public class TypeMetadataSurrogate : TypeMetadataBase
    {
        private readonly TypeMetadataBase _realMetadata;// TODO: change to TypeMetadata type

        #region Constructor

        public TypeMetadataSurrogate( TypeMetadataBase realMetadata )
        {
//            _realMetadata = realMetadata; // TODO: need to change TypeMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public override string TypeName
        {
            get => _realMetadata.TypeName;
            internal set => _realMetadata.TypeName = value;
        }
        [DataMember]
        public override string NamespaceName
        {
            get => _realMetadata.NamespaceName;
            internal set => _realMetadata.NamespaceName = value;
        }
        [DataMember]
        public override TypeMetadataBase BaseType
        {
            get => _realMetadata.BaseType;
            internal set => _realMetadata.BaseType = value;
        }
        [DataMember]
        public override TypeMetadataBase DeclaringType
        {
            get => _realMetadata.DeclaringType;
            internal set => _realMetadata.DeclaringType = value;
        }
        [DataMember]
        public override TypeKind TypeKind
        {
            get => _realMetadata.TypeKind;
            internal set => _realMetadata.TypeKind = value;
        }
        [DataMember]
        public override Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers
        {
            get => _realMetadata.Modifiers;
            internal set => _realMetadata.Modifiers = value;
        }
        [DataMember]
        public override IEnumerable<FieldMetadataBase> Fields
        {
            get => _realMetadata.Fields;
            internal set => _realMetadata.Fields = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> GenericArguments
        {
            get => _realMetadata.GenericArguments;
            internal set => _realMetadata.GenericArguments = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> Attributes
        {
            get => _realMetadata.Attributes;
            internal set => _realMetadata.Attributes = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> ImplementedInterfaces
        {
            get => _realMetadata.ImplementedInterfaces;
            internal set => _realMetadata.ImplementedInterfaces = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> NestedTypes
        {
            get => _realMetadata.NestedTypes;
            internal set => _realMetadata.NestedTypes = value;
        }
        [DataMember]
        public override IEnumerable<PropertyMetadataBase> Properties
        {
            get => _realMetadata.Properties;
            internal set => _realMetadata.Properties = value;
        }
        [DataMember]
        public override IEnumerable<MethodMetadataBase> Methods
        {
            get => _realMetadata.Methods;
            internal set => _realMetadata.Methods = value;
        }
        [DataMember]
        public override IEnumerable<MethodMetadataBase> Constructors
        {
            get => _realMetadata.Constructors;
            internal set => _realMetadata.Constructors = value;
        }
        [DataMember]
        public override IEnumerable<EventMetadataBase> Events
        {
            get => _realMetadata.Events;
            internal set => _realMetadata.Events = value;
        }
        [DataMember]
        public override string FullName
        {
            get => _realMetadata.FullName;
            internal set => _realMetadata.FullName = value;
        }

        #endregion
    }
}