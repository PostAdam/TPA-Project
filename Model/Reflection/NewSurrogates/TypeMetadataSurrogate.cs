using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static Model.Reflection.NewSurrogates.CollectionOryginalTypeAccessor;
using static Model.Reflection.NewSurrogates.CollectionTypeAccessor;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "TypeMetadata" )]
    public class TypeMetadataSurrogate
    {
        #region Constructors

        public TypeMetadataSurrogate( TypeMetadata typeMetadata )
        {
            TypeName = typeMetadata.TypeName;
            NamespaceName = typeMetadata.NamespaceName;
            FullName = typeMetadata.FullName;

            ReproducedSurrogateTypes.Add(
                typeMetadata.FullName ?? typeMetadata.NamespaceName + " . " + typeMetadata.TypeName,
                this );

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
            Fields = GetFieldsMetadata( typeMetadata.Fields );

            GenericArguments = GetTypesMetadata( typeMetadata.GenericArguments );
            Attributes = GetTypesMetadata( typeMetadata.Attributes );
            ImplementedInterfaces = GetTypesMetadata( typeMetadata.ImplementedInterfaces );
            NestedTypes = GetTypesMetadata( typeMetadata.NestedTypes );
            Properties = GetPropertiesMetadata( typeMetadata.Properties );
            Methods = GetMethodsMetadata( typeMetadata.Methods );
            Constructors = GetMethodsMetadata( typeMetadata.Constructors );
            Events = GetEventsMetadata( typeMetadata.Events );
        }

        private TypeMetadataSurrogate( string typeName, string namespaceName )
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        #endregion

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

        #region Emiters

        public static TypeMetadataSurrogate EmitSurrogateTypeMetadata( TypeMetadata typeMetadata )
        {
            if ( typeMetadata == null )
            {
                return null;
            }

            string typeId = typeMetadata.FullName ?? typeMetadata.NamespaceName + " . " + typeMetadata.TypeName;
            if ( !ReproducedSurrogateTypes.ContainsKey( typeId ) )
            {
                new TypeMetadataSurrogate( typeMetadata );
            }

            return ReproducedSurrogateTypes[typeId];
        }

        public TypeMetadata EmitOriginalTypeMetadata()
        {
            string typeId = FullName ?? NamespaceName + " . " + TypeName;
            if ( !ReproducedOriginalTypes.ContainsKey( typeId ) )
            {
                GetOryginalTypeMetadata();
            }

            return ReproducedOriginalTypes[typeId];
        }

        #endregion

        private static readonly ReproducedSurrogateTypes ReproducedSurrogateTypes = ReproducedSurrogateTypes.Instance;
        private static readonly ReproducedOriginalTypes ReproducedOriginalTypes = ReproducedOriginalTypes.Instance;

        #region Help Methods

        private void GetOryginalTypeMetadata()
        {
            TypeMetadata typeMetadata = new TypeMetadata();
            ReproducedOriginalTypes.Add( FullName ?? NamespaceName + " . " + TypeName, typeMetadata );
            PopulateTypeMetadataWithData( typeMetadata );
        }

        private void PopulateTypeMetadataWithData( TypeMetadata typeMetadata )
        {
            typeMetadata.TypeName = TypeName;
            typeMetadata.NamespaceName = NamespaceName;
            typeMetadata.BaseType = BaseType?.EmitOriginalTypeMetadata();
            typeMetadata.DeclaringType = DeclaringType?.EmitOriginalTypeMetadata();
            typeMetadata.TypeKind = TypeKind;
            typeMetadata.Modifiers = Modifiers;
            typeMetadata.Fields = GetOryginalFieldsMetadata( Fields );
            typeMetadata.GenericArguments = GetOryginalTypesMetadata( GenericArguments );
            typeMetadata.Attributes = GetOryginalTypesMetadata( Attributes );
            typeMetadata.ImplementedInterfaces = GetOryginalTypesMetadata( ImplementedInterfaces );
            typeMetadata.NestedTypes = GetOryginalTypesMetadata( NestedTypes );
            typeMetadata.Properties = GetOryginalPropertiesMetadata( Properties );
            typeMetadata.Methods = GetOryginalMethodsMetadata( Methods );
            typeMetadata.Constructors = GetOryginalMethodsMetadata( Constructors );
            typeMetadata.Events = GetOryginalEventsMetadata( Events );
            typeMetadata.FullName = FullName;
        }

        #endregion
    }
}