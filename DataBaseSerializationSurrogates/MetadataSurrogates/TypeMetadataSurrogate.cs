﻿using System;
using System.Collections.Generic;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class TypeMetadataSurrogate
    {
        #region Constructorss

        public TypeMetadataSurrogate()
        {
        }

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
            _modifiers = typeMetadata.Modifiers;
            Fields = GetFieldsMetadata( typeMetadata.Fields );
            GenericArguments = GetTypesMetadata( typeMetadata.GenericArguments );
            Attributes = GetTypesMetadata( typeMetadata.Attributes );
            ImplementedInterfaces = GetTypesMetadata( typeMetadata.ImplementedInterfaces );
            NestedTypes = GetTypesMetadata( typeMetadata.NestedTypes );
            Properties = GetPropertiesMetadata( typeMetadata.Properties );
            Methods = GetMethodsMetadata( typeMetadata.Methods );
            Constructors = GetMethodsMetadata( typeMetadata.Constructors );
            EventSurrogates = GetEventsMetadata( typeMetadata.Events );
        }

        private TypeMetadataSurrogate( string typeName, string namespaceName )
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        #endregion

        #region Properties

        public int? TypeId { get; set; }
        public string TypeName { get; set; }
        public string NamespaceName { get; set; }
        public TypeMetadataSurrogate BaseType { get; set; }
        public TypeMetadataSurrogate DeclaringType { get; set; }
        public TypeKind TypeKind { get; set; }
        public ICollection<FieldMetadataSurrogate> Fields { get; set; }
        public ICollection<TypeMetadataSurrogate> GenericArguments { get; set; }
        public ICollection<TypeMetadataSurrogate> Attributes { get; set; }
        public ICollection<TypeMetadataSurrogate> ImplementedInterfaces { get; set; }
        public ICollection<TypeMetadataSurrogate> NestedTypes { get; set; }
        public ICollection<PropertyMetadataSurrogate> Properties { get; set; }
        public ICollection<MethodMetadataSurrogate> Methods { get; set; }
        public ICollection<MethodMetadataSurrogate> Constructors { get; set; }
        public ICollection<EventMetadataSurrogate> EventSurrogates { get; set; }
        public string FullName { get; set; }
        private Tuple<AccessLevel, SealedEnum, AbstractEnum> _modifiers;

        #region Modifiers

        public AccessLevel? AccessLevel
        {
            get => _modifiers?.Item1;
            set
            {
                if ( value != null )
                    _modifiers =
                        new Tuple<AccessLevel, SealedEnum, AbstractEnum>( 
                            value.Value,
                            _modifiers?.Item2 ?? Model.Reflection.Enums.SealedEnum.NotSealed,
                            _modifiers?.Item3 ?? Model.Reflection.Enums.AbstractEnum.NotAbstract );
            }
        }

        public SealedEnum? SealedEnum
        {
            get => _modifiers?.Item2;
            set
            {
                if ( value != null )
                    _modifiers =
                        new Tuple<AccessLevel, SealedEnum, AbstractEnum>( 
                            _modifiers?.Item1 ?? Model.Reflection.Enums.AccessLevel.Public,
                            value.Value,
                            _modifiers?.Item3 ?? Model.Reflection.Enums.AbstractEnum.NotAbstract );
            }
        }

        public AbstractEnum? AbstractEnum
        {
            get => _modifiers?.Item3;
            set
            {
                if ( value != null )
                    _modifiers =
                        new Tuple<AccessLevel, SealedEnum, AbstractEnum>(
                            _modifiers?.Item1 ?? Model.Reflection.Enums.AccessLevel.Public,
                            _modifiers?.Item2 ?? Model.Reflection.Enums.SealedEnum.NotSealed,
                            value.Value );
            }
        }

        #endregion

        #endregion

        #region Navigation Properties

        public int? NamespaceForeignId { get; set; }
        public NamespaceMetadataSurrogate NamespaceSurrogate { get; set; }

        public ICollection<FieldMetadataSurrogate> FieldTypeSurrogates { get; set; }
        public ICollection<FieldMetadataSurrogate> FieldAttributesType { get; set; }

        public ICollection<MethodMetadataSurrogate> MethodAttributesTypeSurrogates { get; set; }
        public ICollection<MethodMetadataSurrogate> GenericArgumentsTypeSurrogates { get; set; }
        public ICollection<MethodMetadataSurrogate> ReturnTypeSurrogates { get; set; }

        public ICollection<EventMetadataSurrogate> EventTypeSurrogates { get; set; }
        public ICollection<EventMetadataSurrogate> EventAttributesSurrogates { get; set; }

        public ICollection<ParameterMetadataSurrogate> ParameterAttributeSurrogates { get; set; }
        public ICollection<ParameterMetadataSurrogate> ParameterSurrogates { get; set; } 

        public ICollection<PropertyMetadataSurrogate> PropertyAttributesTypes { get; set; } 
        public ICollection<PropertyMetadataSurrogate> PropertyTypeMetadatas { get; set; }


        public int? BaseTypeId { get; set; }
        public ICollection<TypeMetadataSurrogate> BaseTypes { get; set; }
        public int? DeclaringTypeId { get; set; }
        public ICollection<TypeMetadataSurrogate> DeclaringTypes { get; set; }

        public ICollection<TypeMetadataSurrogate> TypeGenericArguments { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeAttributes { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeImplementedInterfaces { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeNestedTypes { get; set; }



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
            typeMetadata.Modifiers = _modifiers;
            typeMetadata.Fields = CollectionOriginalTypeAccessor.GetOriginalFieldsMetadata( Fields );
            typeMetadata.GenericArguments = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( GenericArguments );
            typeMetadata.Attributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( Attributes );
            typeMetadata.ImplementedInterfaces =
                CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( ImplementedInterfaces );
            typeMetadata.NestedTypes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( NestedTypes );
            typeMetadata.Properties = CollectionOriginalTypeAccessor.GetOriginalPropertiesMetadata( Properties );
            typeMetadata.Methods = CollectionOriginalTypeAccessor.GetOriginalMethodsMetadata( Methods );
            typeMetadata.Constructors = CollectionOriginalTypeAccessor.GetOriginalMethodsMetadata( Constructors );
            typeMetadata.Events = CollectionOriginalTypeAccessor.GetOriginalEventsMetadata( EventSurrogates );
            typeMetadata.FullName = FullName;
        }

        #endregion
    }
}