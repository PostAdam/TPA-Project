﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
{
    public class TypeMetadata : TypeMetadataBase
    {
        #region Properties

        public override string TypeName { get; internal set; }
        public override string NamespaceName { get; internal set; }
        public override TypeMetadataBase BaseType { get; internal set; }
        public override TypeMetadataBase DeclaringType { get; internal set; }
        public override TypeKind TypeKind { get; internal set; }
        public override Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; internal set; }
        public override IEnumerable<FieldMetadataBase> Fields { get; internal set; }
        public override IEnumerable<TypeMetadataBase> GenericArguments { get; internal set; }
        public override IEnumerable<TypeMetadataBase> Attributes { get; internal set; }
        public override IEnumerable<TypeMetadataBase> ImplementedInterfaces { get; internal set; }
        public override IEnumerable<TypeMetadataBase> NestedTypes { get; internal set; }
        public override IEnumerable<PropertyMetadataBase> Properties { get; internal set; }
        public override IEnumerable<MethodMetadataBase> Methods { get; internal set; }
        public override IEnumerable<MethodMetadataBase> Constructors { get; internal set; }
        public override IEnumerable<EventMetadataBase> Events { get; internal set; }
        public override string FullName { get; internal set; }

        const BindingFlags AllAccessLevels = BindingFlags.NonPublic
                                             | BindingFlags.DeclaredOnly
                                             | BindingFlags.Public
                                             | BindingFlags.Static
                                             | BindingFlags.Instance;

        #endregion

        #region Constructors

        public TypeMetadata()
        {
        }

        internal TypeMetadata( Type type )
        {
            // Infos
            TypeName = type.Name;
            FullName = type.FullName ?? type.Namespace + "." + type.Name;
            Modifiers = EmitModifiers( type );
            TypeKind = GetTypeKind( type );

            // Types
            BaseType = EmitExtends( type.GetTypeInfo().BaseType );
            ImplementedInterfaces = EmitTypes( type.GetInterfaces() );
            DeclaringType = EmitType( type.DeclaringType );
            NestedTypes = EmitTypes( type.GetNestedTypes( AllAccessLevels ) );
            Attributes = EmitAttributes( type.GetCustomAttributes( false ).Cast<Attribute>() );
            Fields = EmitFields( type.GetFields( AllAccessLevels ) );
            Events = EmitEvents( type.GetEvents( AllAccessLevels ) );
            GenericArguments = type.IsGenericTypeDefinition ? EmitGenericArguments( type.GetGenericArguments() ) : null;
            Properties = EmitProperties( type.GetProperties( AllAccessLevels ) );

            // Methods
            Constructors = MethodMetadata.EmitMethods( type.GetConstructors( AllAccessLevels ) );
            Methods = MethodMetadata.EmitMethods( type.GetMethods( AllAccessLevels ) );
        }

        internal static IEnumerable<TypeMetadata> EmitGenericArguments( IEnumerable<Type> arguments )
        {
            return from Type _argument in arguments select EmitReference( _argument );
        }

        #endregion

        /*#region Emit API

        internal static IEnumerable<TypeMetadata> EmitTypes( IEnumerable<Type> types )
        {
            return from type in types select EmitType( type );
        }

        internal static TypeMetadata EmitType( Type type )
        {
            if ( type == null )
            {
                return null;
            }

            if ( !ReflectedTypes.ContainsKey( type.FullName ?? type.Namespace + " . " + type.Name ) )
            {
                ReflectedTypes.Add( type.FullName ?? type.Namespace + " . " + type.Name,
                    new TypeMetadata( type ) );
            }

            return ReflectedTypes[ type.FullName ?? type.Namespace + " . " + type.Name ];
        }

        internal static IEnumerable<TypeMetadata> EmitAttributes( IEnumerable<Attribute> attributes )
        {
            if ( attributes == null )
                return null;
            IEnumerable<Type> attributesTypes = from attribute in attributes select attribute.GetType();
            return EmitTypes( attributesTypes );
        }

        #endregion

        private static readonly ReflectedTypes ReflectedTypes = ReflectedTypes.Instance;*/

        #region Private Constructors

        private TypeMetadata( string typeName, string namespaceName )
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        private TypeMetadata( string typeName, string namespaceName,
            IEnumerable<TypeMetadata> genericArguments ) :
            this( typeName, namespaceName )
        {
            GenericArguments = genericArguments;
        }

        #endregion

        #region Private Methods

        private static IEnumerable<FieldMetadata> EmitFields( IEnumerable<FieldInfo> fieldsInfo )
        {
            return from fieldInfo in fieldsInfo select new FieldMetadata( fieldInfo );
        }

        private static IEnumerable<EventMetadata> EmitEvents( IEnumerable<EventInfo> events )
        {
            return from singleEvent in events select new EventMetadata( singleEvent );
        }

        private static IEnumerable<PropertyMetadata> EmitProperties( IEnumerable<PropertyInfo> props )
        {
            return from prop in props select new PropertyMetadata( prop );
        }

        private static TypeKind GetTypeKind( Type type )
        {
            return type.IsEnum ? TypeKind.Enum :
                type.IsValueType ? TypeKind.Struct :
                type.IsInterface ? TypeKind.Interface :
                TypeKind.Class;
        }

        private static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers( Type type )
        {
            AccessLevel access = AccessLevel.Private;
            if ( type.IsPublic )
                access = AccessLevel.Public;
            else if ( type.IsNestedPublic )
                access = AccessLevel.Public;
            else if ( type.IsNestedFamily )
                access = AccessLevel.Protected;
            else if ( type.IsNestedFamANDAssem )
                access = AccessLevel.Internal;

            SealedEnum _sealed = SealedEnum.NotSealed;
            if ( type.IsSealed )
                _sealed = SealedEnum.Sealed;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if ( type.IsAbstract )
                _abstract = AbstractEnum.Abstract;

            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>( access, _sealed, _abstract );
        }

        private static TypeMetadata EmitReference( Type type )
        {
            if ( !type.IsGenericType )
            {
                return new TypeMetadata( type.Name, type.GetNamespace() );
            }

            return new TypeMetadata( type.Name, type.GetNamespace(),
                EmitGenericArguments( type.GetGenericArguments() ) );
        }

        private static TypeMetadata EmitExtends( Type baseType )
        {
            if ( baseType == null || baseType == typeof( Object ) || baseType == typeof( ValueType ) ||
                baseType == typeof( Enum ) )
            {
                return null;
            }

            return EmitReference( baseType );
        }

        #endregion
    }
}