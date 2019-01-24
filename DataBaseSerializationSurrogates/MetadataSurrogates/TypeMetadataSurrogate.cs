using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static DataBaseSerializationSurrogates.CollectionOriginalTypeAccessor;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class TypeMetadataSurrogate 
    {
        #region Constructors

        public TypeMetadataSurrogate()
        {
        }

        public TypeMetadataSurrogate( TypeMetadataBase typeMetadata )
        {
            TypeName = typeMetadata.TypeName;
            NamespaceName = typeMetadata.NamespaceName;
            FullName = typeMetadata.FullName ?? typeMetadata.NamespaceName + "." + typeMetadata.TypeName;

            ReproducedSurrogateTypes.Add( FullName, this );

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
            FullName = namespaceName + "." + typeName;
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
                            _modifiers?.Item2 ?? ModelBase.Enums.SealedEnum.NotSealed,
                            _modifiers?.Item3 ?? ModelBase.Enums.AbstractEnum.NotAbstract );
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
                            _modifiers?.Item1 ?? ModelBase.Enums.AccessLevel.Public,
                            value.Value,
                            _modifiers?.Item3 ?? ModelBase.Enums.AbstractEnum.NotAbstract );
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
                            _modifiers?.Item1 ?? ModelBase.Enums.AccessLevel.Public,
                            _modifiers?.Item2 ?? ModelBase.Enums.SealedEnum.NotSealed,
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
            string typeId = FullName;
            if ( !ReproducedOriginalTypes.ContainsKey( typeId ) )
            {
                GetOriginalTypeMetadata();
            }

            return ReproducedOriginalTypes[typeId];
        }

        #endregion

        #region Private Fields

        private static readonly ReproducedSurrogateTypes ReproducedSurrogateTypes = ReproducedSurrogateTypes.Instance;
        private static readonly ReproducedOriginalTypes ReproducedOriginalTypes = ReproducedOriginalTypes.Instance; 

        #endregion

        #region Help Methods

        private void GetOriginalTypeMetadata()
        {
            TypeMetadataBase typeMetadata = new TypeMetadataBase();
            ReproducedOriginalTypes.Add( FullName, typeMetadata );
            PopulateTypeMetadataWithData( typeMetadata );
        }

        private void PopulateTypeMetadataWithData( TypeMetadataBase typeMetadata )
        {
            typeMetadata.TypeName = TypeName;
            typeMetadata.NamespaceName = NamespaceName;
            typeMetadata.BaseType = BaseType?.EmitOriginalTypeMetadata();
            typeMetadata.DeclaringType = DeclaringType?.EmitOriginalTypeMetadata();
            typeMetadata.TypeKind = TypeKind;
            typeMetadata.Modifiers = _modifiers;
            typeMetadata.Fields = GetOriginalFieldsMetadata( Fields );
            typeMetadata.GenericArguments = GetOriginalTypesMetadata( GenericArguments );
            typeMetadata.Attributes = GetOriginalTypesMetadata( Attributes );
            typeMetadata.ImplementedInterfaces = GetOriginalTypesMetadata( ImplementedInterfaces );
            typeMetadata.NestedTypes = GetOriginalTypesMetadata( NestedTypes );
            typeMetadata.Properties = GetOriginalPropertiesMetadata( Properties );
            typeMetadata.Methods = GetOriginalMethodsMetadata( Methods );
            typeMetadata.Constructors = GetOriginalMethodsMetadata( Constructors );
            typeMetadata.Events = GetOriginalEventsMetadata( EventSurrogates );
            typeMetadata.FullName = FullName;
        }

        #endregion
    }
}