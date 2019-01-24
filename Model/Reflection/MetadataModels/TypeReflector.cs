using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.ModelDTG;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
{
    public class TypeReflector
    {

        internal static IEnumerable<TypeMetadata> EmitGenericArguments( IEnumerable<Type> arguments )
        {
            return from Type _argument in arguments select EmitReference( _argument );
        }


        #region Emit API

        internal static IEnumerable<TypeMetadata> EmitTypes( IEnumerable<Type> types )
        {
            return from type in types select EmitType( type );
        }

        internal static IEnumerable<TypeMetadata> EmitAttributeTypes( IEnumerable<Type> types )
        {
            return from type in types select EmitAttributeType( type );
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

        internal static TypeMetadata EmitAttributeType( Type type )
        {
            if ( type == null )
            {
                return null;
            }

            if ( !ReflectedTypes.ContainsKey( type.FullName ?? type.Namespace + " . " + type.Name ) )
            {
                ReflectedTypes.Add( type.FullName ?? type.Namespace + " . " + type.Name,
                    new TypeMetadata( type.Namespace, type.Name, type.FullName ) );
            }

            return ReflectedTypes[ type.FullName ?? type.Namespace + " . " + type.Name ];
        }

        internal static IEnumerable<TypeMetadata> EmitAttributes( IEnumerable<CustomAttributeData> attributes )
        {
            if ( attributes == null )
                return null;
            IEnumerable<Type> attributesTypes = from attribute in attributes select attribute.GetType();
            return EmitAttributeTypes( attributesTypes );
        }

        #endregion

        internal static readonly ReflectedTypes ReflectedTypes = ReflectedTypes.Instance;


        internal static TypeMetadata EmitReference( Type type )
        {
            if ( !type.IsGenericType )
            {
                return new TypeMetadata( type.Name, type.GetNamespace(), type.FullName );
            }

            return new TypeMetadata( type.Name, type.GetNamespace(),
                EmitGenericArguments( type.GetGenericArguments() ) );
        }

        internal static TypeMetadata EmitExtends( Type baseType )
        {
            if ( baseType == null || baseType == typeof( Object ) || baseType == typeof( ValueType ) ||
                 baseType == typeof( Enum ) )
            {
                return null;
            }

            return EmitReference( baseType );
        }

        #region Private Methods

        internal static IEnumerable<FieldMetadata> EmitFields( IEnumerable<FieldInfo> fieldsInfo )
        {
            return from fieldInfo in fieldsInfo select new FieldMetadata( fieldInfo );
        }

        internal static IEnumerable<EventMetadata> EmitEvents( IEnumerable<EventInfo> events )
        {
            return from singleEvent in events select new EventMetadata( singleEvent );
        }

        internal static IEnumerable<PropertyMetadata> EmitProperties( IEnumerable<PropertyInfo> props )
        {
            return from prop in props select new PropertyMetadata( prop );
        }

        internal static TypeKind GetTypeKind( Type type )
        {
            return type.IsEnum ? TypeKind.Enum :
                type.IsValueType ? TypeKind.Struct :
                type.IsInterface ? TypeKind.Interface :
                TypeKind.Class;
        }

        internal static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers( Type type )
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


        #endregion
    }
}