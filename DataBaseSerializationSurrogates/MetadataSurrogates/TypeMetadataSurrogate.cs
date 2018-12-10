using System;
using System.Collections.Generic;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
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
            _modifiers = typeMetadata.Modifiers;
            Fields = GetFieldsMetadata( typeMetadata.Fields ) as ICollection<FieldMetadataSurrogate>;
            GenericArguments = GetTypesMetadata( typeMetadata.GenericArguments ) as ICollection<TypeMetadataSurrogate>;
            Attributes = GetTypesMetadata( typeMetadata.Attributes ) as ICollection<TypeMetadataSurrogate>;
            ImplementedInterfaces = GetTypesMetadata( typeMetadata.ImplementedInterfaces ) as ICollection<TypeMetadataSurrogate>;
            NestedTypes = GetTypesMetadata( typeMetadata.NestedTypes ) as ICollection<TypeMetadataSurrogate>;
            Properties = GetPropertiesMetadata( typeMetadata.Properties ) as ICollection<PropertyMetadataSurrogate>;
            Methods = GetMethodsMetadata( typeMetadata.Methods ) as ICollection<MethodMetadataSurrogate>;
            Constructors = GetMethodsMetadata( typeMetadata.Constructors ) as ICollection<MethodMetadataSurrogate>;
            Events = GetEventsMetadata( typeMetadata.Events ) as ICollection<EventMetadataSurrogate>;
        }

        private TypeMetadataSurrogate( string typeName, string namespaceName )
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        #endregion

        #region Properties

        public int TypeId { get; set; }
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
        public ICollection<EventMetadataSurrogate> Events { get; set; }
        public string FullName { get; set; }
        private Tuple<AccessLevel, SealedEnum, AbstractEnum> _modifiers;

        #region Modifiers

        public AccessLevel AccessLevel
        {
            get => _modifiers.Item1;
            set => _modifiers = new Tuple<AccessLevel, SealedEnum, AbstractEnum>( value, _modifiers.Item2, _modifiers.Item3 );
        }
        public SealedEnum SealedEnum
        {
            get => _modifiers.Item2;
            set => _modifiers = new Tuple<AccessLevel, SealedEnum, AbstractEnum>( _modifiers.Item1, value, _modifiers.Item3 );
        }
        public AbstractEnum AbstractEnum
        {
            get => _modifiers.Item3;
            set => _modifiers = new Tuple<AccessLevel, SealedEnum, AbstractEnum>( _modifiers.Item1, _modifiers.Item2, value );
        }

        #endregion

        #endregion

        #region Navigation Properties

        public NamespaceMetadataSurrogate NamespaceMetadataSurrogate { get; set; }

        public ICollection<FieldMetadataSurrogate> FieldTypeSurrogates { get; set; }
        public ICollection<FieldMetadataSurrogate> FieldAttributesType { get; set; }

        public ICollection<MethodMetadataSurrogate> MethodAttributesTypeSurrogates { get; set; }
        public ICollection<MethodMetadataSurrogate> GenericArgumentsTypeSurrogates { get; set; }
        public ICollection<MethodMetadataSurrogate> ReturnTypeSurrogates { get; set; }

        public ICollection<EventMetadataSurrogate> EventTypeSurrogates { get; set; }
        public ICollection<EventMetadataSurrogate> EventAttributesSurrogates { get; set; }

        public ICollection<ParameterMetadataSurrogate> ParameterAttributeSurrogates { get; set; }
        public ICollection<ParameterMetadataSurrogate> ParameterMetadataSurrogates { get; set; } 

        public ICollection<PropertyMetadataSurrogate> PropertyAttributesTypes { get; set; } 
        public ICollection<PropertyMetadataSurrogate> PropertyTypeMetadatas { get; set; } 

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
            typeMetadata.Events = CollectionOriginalTypeAccessor.GetOriginalEventsMetadata( Events );
            typeMetadata.FullName = FullName;
        }

        #endregion
    }
}