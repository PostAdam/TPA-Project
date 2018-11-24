using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogate.MetadataSurrogates
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
                GetOriginalTypeMetadata();
            }

            return ReproducedOriginalTypes[typeId];
        }

        #endregion

        private static readonly ReproducedSurrogateTypes ReproducedSurrogateTypes = ReproducedSurrogateTypes.Instance;
        private static readonly ReproducedOriginalTypes ReproducedOriginalTypes = ReproducedOriginalTypes.Instance;

        #region Help Methods

        private void GetOriginalTypeMetadata()
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
            typeMetadata.Fields = CollectionOriginalTypeAccessor.GetOriginalFieldsMetadata( Fields );
            typeMetadata.GenericArguments = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( GenericArguments );
            typeMetadata.Attributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( Attributes );
            typeMetadata.ImplementedInterfaces = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( ImplementedInterfaces );
            typeMetadata.NestedTypes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( NestedTypes );
            typeMetadata.Properties = CollectionOriginalTypeAccessor.GetOriginalPropertiesMetadata( Properties );
            typeMetadata.Methods = CollectionOriginalTypeAccessor.GetOriginalMethodsMetadata( Methods );
            typeMetadata.Constructors = CollectionOriginalTypeAccessor.GetOriginalMethodsMetadata( Constructors );
            typeMetadata.Events = CollectionOriginalTypeAccessor.GetOriginalEventsMetadata( Events );
            typeMetadata.FullName = FullName;
        }

        #endregion
    }
}