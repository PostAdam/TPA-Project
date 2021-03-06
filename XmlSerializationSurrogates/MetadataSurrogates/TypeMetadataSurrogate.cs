﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using ModelBase;
using ModelBase.Enums;
using static XmlSerializationSurrogates.CollectionOriginalTypeAccessor;
using static XmlSerializationSurrogates.CollectionTypeAccessor;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( IsReference = true, Name = "TypeReflector" )]
    public class TypeMetadataSurrogate
    {
        #region Constructors

        public TypeMetadataSurrogate( TypeMetadataBase typeMetadata )
        {
            TypeName = typeMetadata.TypeName;
            NamespaceName = typeMetadata.NamespaceName;
            FullName = typeMetadata.FullName ?? typeMetadata.NamespaceName + "." + typeMetadata.TypeName;

            ReproducedSurrogateTypes.Add( FullName, this );

            if ( typeMetadata.BaseType != null )
            {
                BaseType = new TypeMetadataSurrogate( typeMetadata.BaseType.TypeName,
                    typeMetadata.BaseType.NamespaceName, typeMetadata.BaseType.FullName );
            }

            if ( typeMetadata.DeclaringType != null )
            {
                DeclaringType = new TypeMetadataSurrogate( typeMetadata.DeclaringType.TypeName,
                    typeMetadata.DeclaringType.NamespaceName, typeMetadata.DeclaringType.FullName );
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

        private TypeMetadataSurrogate( string typeName, string namespaceName, string fullName )
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
            FullName = fullName ?? namespaceName + "." + typeName;
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

        public static TypeMetadataSurrogate EmitSurrogateTypeMetadata( TypeMetadataBase typeMetadata )
        {
            if ( typeMetadata == null )
            {
                return null;
            }

            string typeId = typeMetadata.FullName ?? typeMetadata.NamespaceName + "." + typeMetadata.TypeName;
            if ( !ReproducedSurrogateTypes.ContainsKey( typeId ) )
            {
                new TypeMetadataSurrogate( typeMetadata );
            }

            return ReproducedSurrogateTypes[typeId];
        }

        public TypeMetadataBase EmitOriginalTypeMetadata()
        {
            string typeId = FullName ?? NamespaceName + "." + TypeName;
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
            TypeMetadataBase typeMetadata = new TypeMetadataBase();
            ReproducedOriginalTypes.Add( FullName ?? NamespaceName + "." + TypeName, typeMetadata );
            PopulateTypeMetadataWithData( typeMetadata );
        }

        private void PopulateTypeMetadataWithData( TypeMetadataBase typeMetadata )
        {
            typeMetadata.TypeName = TypeName;
            typeMetadata.NamespaceName = NamespaceName;
            typeMetadata.FullName = FullName;
            typeMetadata.BaseType = BaseType?.EmitOriginalTypeMetadata();
            typeMetadata.DeclaringType = DeclaringType?.EmitOriginalTypeMetadata();
            typeMetadata.TypeKind = TypeKind;
            typeMetadata.Modifiers = Modifiers;
            typeMetadata.Fields = GetOriginalFieldsMetadata( Fields );
            typeMetadata.GenericArguments = GetOriginalTypesMetadata( GenericArguments );
            typeMetadata.Attributes = GetOriginalTypesMetadata( Attributes );
            typeMetadata.ImplementedInterfaces = GetOriginalTypesMetadata( ImplementedInterfaces );
            typeMetadata.NestedTypes = GetOriginalTypesMetadata( NestedTypes );
            typeMetadata.Properties = GetOriginalPropertiesMetadata( Properties );
            typeMetadata.Methods = GetOriginalMethodsMetadata( Methods );
            typeMetadata.Constructors = GetOriginalMethodsMetadata( Constructors );
            typeMetadata.Events = GetOriginalEventsMetadata( Events );
        }

        #endregion
    }
}