using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "TypeMetadata" )]
    public class TypeMetadataSurrogate
    {
        #region Constructor

        public TypeMetadataSurrogate( TypeMetadata typeMetadata )
        {
            TypeName = typeMetadata.TypeName;
            NamespaceName = typeMetadata.NamespaceName;
            if ( typeMetadata.BaseType != null )
            {
                BaseType = new TypeMetadataSurrogate( typeMetadata.BaseType.TypeName,
                    typeMetadata.BaseType.NamespaceName );
            }

            if ( typeMetadata.DeclaringType != null )
            {
                DeclaringType = new TypeMetadataSurrogate( typeMetadata.DeclaringType.TypeName,
                    typeMetadata.DeclaringType.NamespaceName );
            }

            TypeKind = typeMetadata.TypeKind;
            Modifiers = typeMetadata.Modifiers;
            Fields = CollectionTypeAccessor.GetFieldsMetadata( typeMetadata.Fields.ToList() );

            GenericArguments = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.GenericArguments.ToList() );
            Attributes = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.Attributes.ToList() );
            ImplementedInterfaces = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.ImplementedInterfaces.ToList() );
            NestedTypes = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.NestedTypes.ToList() );
            Properties = CollectionTypeAccessor.GetPropertiesMetadata( typeMetadata.Properties.ToList() );
            Methods = CollectionTypeAccessor.GetMethodsMetadata( typeMetadata.Methods.ToList() );
            Constructors = CollectionTypeAccessor.GetMethodsMetadata( typeMetadata.Constructors.ToList() );
            Events = CollectionTypeAccessor.GetEventsMetadata( typeMetadata.Events.ToList() );
            FullName = typeMetadata.FullName;
        }

        public TypeMetadataSurrogate( string typeName, string namespaceName )
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        #endregion

        private readonly ReproducedTypes _reproducedTypes = ReproducedTypes.Instance;

        #region Properties

        [DataMember]
        public string TypeName { get; set; }

        [DataMember]
        public string NamespaceName { get; set; }

        [DataMember]
        public TypeMetadataSurrogate BaseType { get; set; }

        [DataMember]
        public TypeMetadataSurrogate DeclaringType { get; set; }

        [DataMember]
        public TypeKind TypeKind { get; set; }

        [DataMember]
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }

        [DataMember]
        public IEnumerable<FieldMetadataSurrogate> Fields { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> GenericArguments { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> Attributes { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> ImplementedInterfaces { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> NestedTypes { get; set; }

        [DataMember]
        public IEnumerable<PropertyMetadataSurrogate> Properties { get; set; }

        [DataMember]
        public IEnumerable<MethodMetadataSurrogate> Methods { get; set; }

        [DataMember]
        public IEnumerable<MethodMetadataSurrogate> Constructors { get; set; }

        [DataMember]
        public IEnumerable<EventMetadataSurrogate> Events { get; set; }

        [DataMember]
        public string FullName { get; set; }

        #endregion

        public TypeMetadata GetOryginalTypeMetadata()
        {
            return new TypeMetadata
            {
                TypeName = TypeName,
                NamespaceName = NamespaceName,
                BaseType = BaseType.GetOryginalTypeMetadata(),
                DeclaringType = DeclaringType.GetOryginalTypeMetadata(),
                TypeKind = TypeKind,
                Modifiers = Modifiers,
                Fields = CollectionOryginalTypeAccessor.GetOryginalFieldsMetadata( Fields ),
                GenericArguments = CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( GenericArguments ),
                Attributes = CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( Attributes ),
                ImplementedInterfaces =
                    CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( ImplementedInterfaces ),
                NestedTypes = CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( NestedTypes ),
                Properties = CollectionOryginalTypeAccessor.GetOryginalPropertiesMetadata( Properties ),
                Methods = CollectionOryginalTypeAccessor.GetOryginalMethodsMetadata( Methods ),
                Constructors = CollectionOryginalTypeAccessor.GetOryginalMethodsMetadata( Constructors ),
                Events = CollectionOryginalTypeAccessor.GetOryginalEventsMetadata( Events ),
                FullName = FullName
            };
        }
    }
}