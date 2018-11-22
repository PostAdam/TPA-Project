using System;
using System.Collections.Generic;
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
            FullName = typeMetadata.FullName;

            _reproducedTypes.Add( typeMetadata.FullName ?? typeMetadata.NamespaceName + " . " + typeMetadata.TypeName,
                this );
            //_reproducedTypes[ typeMetadata.FullName ?? typeMetadata.NamespaceName + " . " + typeMetadata.TypeName ] = this;

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
            Fields = CollectionTypeAccessor.GetFieldsMetadata( typeMetadata.Fields );

            GenericArguments = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.GenericArguments );
            Attributes = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.Attributes );
            ImplementedInterfaces = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.ImplementedInterfaces );
            NestedTypes = CollectionTypeAccessor.GetTypesMetadata( typeMetadata.NestedTypes );
            Properties = CollectionTypeAccessor.GetPropertiesMetadata( typeMetadata.Properties );
            Methods = CollectionTypeAccessor.GetMethodsMetadata( typeMetadata.Methods );
            Constructors = CollectionTypeAccessor.GetMethodsMetadata( typeMetadata.Constructors );
            Events = CollectionTypeAccessor.GetEventsMetadata( typeMetadata.Events );
        }

        private TypeMetadataSurrogate( string typeName, string namespaceName )
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        #endregion

        private static readonly ReproducedTypes _reproducedTypes = ReproducedTypes.Instance;

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

        public static TypeMetadataSurrogate GetType( TypeMetadata typeMetadata )
        {
            if ( typeMetadata == null )
            {
                return null;
            }

            string typeId = typeMetadata.FullName ?? typeMetadata.NamespaceName + " . " + typeMetadata.TypeName;
            if ( !_reproducedTypes.ContainsKey( typeId ) )
            {
                
                try
                {
//                    _reproducedTypes.Add( typeId, new TypeMetadataSurrogate( typeMetadata ) );
                    new TypeMetadataSurrogate( typeMetadata );
                }
                catch ( ArgumentException e )
                {

                    Console.WriteLine( "Type : " + typeId + "\n" );
                    foreach ( var reproducedType in _reproducedTypes )
                    {
                        Console.WriteLine( "Type : " + reproducedType.Value.FullName ?? reproducedType.Value.NamespaceName + " . " + reproducedType.Value.TypeName );
                    }
                    throw;
                }
            }

            return _reproducedTypes[ typeId ];
        }

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