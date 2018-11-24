using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

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

        public string TypeName { get; set; }


        public string NamespaceName { get; set; }


        public TypeMetadataSurrogate BaseType { get; set; }


        public TypeMetadataSurrogate DeclaringType { get; set; }


        public TypeKind TypeKind { get; set; }

        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }

        public virtual IEnumerable<FieldMetadataSurrogate> Fields { get; set; }

        public virtual IEnumerable<TypeMetadataSurrogate> GenericArguments { get; set; }

        public virtual IEnumerable<TypeMetadataSurrogate> Attributes { get; set; }

        public virtual IEnumerable<TypeMetadataSurrogate> ImplementedInterfaces { get; set; }

        public virtual IEnumerable<TypeMetadataSurrogate> NestedTypes { get; set; }

        public virtual IEnumerable<PropertyMetadataSurrogate> Properties { get; set; }

        public virtual IEnumerable<MethodMetadataSurrogate> Methods { get; set; }

        public virtual IEnumerable<MethodMetadataSurrogate> Constructors { get; set; }

        public virtual IEnumerable<EventMetadataSurrogate> Events { get; set; }

        [Key]
        public string FullName { get; set; }

        public virtual FieldMetadataSurrogate Field { get; set; }
        public virtual TypeMetadataSurrogate GenericArgument { get; set; }
        public virtual TypeMetadataSurrogate Attribute { get; set; }
        public virtual TypeMetadataSurrogate ImplementedInterface { get; set; }
        public virtual TypeMetadataSurrogate NestedType { get; set; }
        public virtual PropertyMetadataSurrogate Property { get; set; }
        public virtual MethodMetadataSurrogate Method { get; set; }
        public virtual MethodMetadataSurrogate Constructor { get; set; }
        public virtual EventMetadataSurrogate Event { get; set; }

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