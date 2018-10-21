using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class TypeMetadata
    {
        #region Constructor

        internal TypeMetadata(Type type)
        {
            // Types
            BaseType = EmitExtends(type);
            DeclaringType = EmitType(type.DeclaringType);
            NestedTypes = EmitTypes(type.GetNestedTypes(AllAccessLevels));
            Attributes = EmitAttributes(type.GetCustomAttributes(false).Cast<Attribute>());
            Fields = EmitFields(type.GetFields(AllAccessLevels));
            Events = EmitEvents(type.GetEvents(AllAccessLevels));
            GenericArguments = type.IsGenericTypeDefinition ? EmitTypes(type.GetGenericArguments()) : null;
            Properties = EmitProperties(type.GetProperties(AllAccessLevels));
            ImplementedInterfaces = EmitTypes(type.GetInterfaces());

            // Methods
            Constructors = MethodMetadata.EmitMethods(type.GetConstructors(AllAccessLevels));
            Methods = MethodMetadata.EmitMethods(type.GetMethods(AllAccessLevels));

            // Infos
            TypeName = type.Name;
            Modifiers = EmitModifiers(type);
            TypeKind = GetTypeKind(type);
        }

        #endregion

        #region API

        internal static TypeMetadata EmitReference(Type type)
        {
            if (!type.IsGenericType)
            {
                return new TypeMetadata(type.Name, type.GetNamespace());
            }
            else
            {
                return new TypeMetadata(type.Name, type.GetNamespace(),
                    EmitTypes(type.GetGenericArguments()));
            }
        }

        internal static IEnumerable<TypeMetadata> EmitTypes(IEnumerable<Type> types)
        {
            return from type in types select EmitType(type);
        }

        public static TypeMetadata EmitType(Type type)
        {
            if (type == null)
            {
                return null;
            }

            if (!TypesDictionary.ReflectedTypes.ContainsKey(type.Name))
            {
                TypesDictionary.ReflectedTypes.Add(type.Name, new TypeMetadata(type));
            }

            return TypesDictionary.ReflectedTypes[type.Name];
        }

        internal static IEnumerable<TypeMetadata> EmitAttributes(IEnumerable<Attribute> attributes)
        {
            if (attributes == null) return null;
            var attributesTypes = from attribute in attributes select attribute.GetType();
            return EmitTypes(attributesTypes);
        }

        #endregion

        #region Variables

        [DataMember] internal string TypeName;
        [DataMember] internal string NamespaceName;
        [DataMember] internal TypeMetadata BaseType;
        [DataMember] internal TypeMetadata DeclaringType;
        [DataMember] internal TypeKind TypeKind;
        [DataMember] internal Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;
        [DataMember] internal IEnumerable<FieldMetadata> Fields;
        [DataMember] internal IEnumerable<TypeMetadata> GenericArguments;
        [DataMember] internal IEnumerable<TypeMetadata> Attributes;
        [DataMember] internal IEnumerable<TypeMetadata> ImplementedInterfaces;
        [DataMember] internal IEnumerable<TypeMetadata> NestedTypes;
        [DataMember] internal IEnumerable<PropertyMetadata> Properties;
        [DataMember] internal IEnumerable<MethodMetadata> Methods;
        [DataMember] internal IEnumerable<MethodMetadata> Constructors;
        [DataMember] internal IEnumerable<EventMetadata> Events;

        public const BindingFlags AllAccessLevels = BindingFlags.NonPublic
                                                    | BindingFlags.DeclaredOnly
                                                    | BindingFlags.Public
                                                    | BindingFlags.Static
                                                    | BindingFlags.Instance;

        #endregion

        #region Private

        #region Private Constructors

        private TypeMetadata(string typeName, string namespaceName)
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
        }

        private TypeMetadata(string typeName, string namespaceName,
            IEnumerable<TypeMetadata> genericArguments) :
            this(typeName, namespaceName)
        {
            GenericArguments = genericArguments;
        }

        #endregion

        #region Methods

        private static IEnumerable<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fieldsInfo)
        {
            return from fieldInfo in fieldsInfo select new FieldMetadata(fieldInfo);
        }

        private static IEnumerable<EventMetadata> EmitEvents(IEnumerable<EventInfo> events)
        {
            return from singleEvent in events select new EventMetadata(singleEvent);
        }

        internal static IEnumerable<PropertyMetadata> EmitProperties(IEnumerable<PropertyInfo> props)
        {
            return from prop in props select new PropertyMetadata(prop);
        }


        private static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.Enum :
                type.IsValueType ? TypeKind.Struct :
                type.IsInterface ? TypeKind.Interface :
                TypeKind.Class;
        }

        internal static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            AccessLevel access = AccessLevel.Private;
            if (type.IsPublic)
                access = AccessLevel.Public;
            else if (type.IsNestedPublic)
                access = AccessLevel.Public;
            else if (type.IsNestedFamily)
                access = AccessLevel.Protected;
            else if (type.IsNestedFamANDAssem)
                access = AccessLevel.Internal;

            SealedEnum _sealed = SealedEnum.NotSealed;
            if (type.IsSealed) _sealed = SealedEnum.Sealed;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (type.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            return new Tuple<AccessLevel, SealedEnum, AbstractEnum>(access, _sealed, _abstract);
        }

        private static TypeMetadata EmitExtends(Type baseType)
        {
            if (baseType == null || baseType == typeof(Object) || baseType == typeof(ValueType) ||
                baseType == typeof(Enum))
            {
                return null;
            }
            else
            {
                return EmitReference(baseType);
            }
        }

        #endregion

        #endregion
    }
}