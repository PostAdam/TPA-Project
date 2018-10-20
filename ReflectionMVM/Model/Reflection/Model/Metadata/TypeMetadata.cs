using System;
using System.Collections.Generic;
using System.Linq;
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
            if (TypesDictionary.ReflectedTypes.ContainsKey(type.Name) == false)
            {
                TypesDictionary.ReflectedTypes.Add(type.Name, this);
            }
            
            TypeName = type.Name;
            DeclaringType = EmitDeclaringType(type.DeclaringType);
            Constructors = MethodMetadata.EmitMethods(type.GetConstructors());
            Methods = MethodMetadata.EmitMethods(type.GetMethods());
            NestedTypes = EmitNestedTypes(type.GetNestedTypes());
            ImplementedInterfaces = EmitImplements(type.GetInterfaces());
            GenericArguments = type.IsGenericTypeDefinition
                ? EmitGenericArguments(type.GetGenericArguments())
                : null;
            Modifiers = EmitModifiers(type);
            BaseType = EmitExtends(type);
            Events = EmitEvents(type.GetEvents());
            Properties = PropertyMetadata.EmitProperties(type.GetProperties());
            Fields = EmitFields(type.GetFields());
            TypeKind = GetTypeKind(type);
            Attributes = EmitAttributes(type.GetCustomAttributes(false).Cast<Attribute>());
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
                    EmitGenericArguments(type.GetGenericArguments()));
            }
        }

        internal static IEnumerable<TypeMetadata> EmitGenericArguments(IEnumerable<Type> arguments)
        {
            return from Type argument in arguments select EmitReference(argument);
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
        [DataMember] internal IEnumerable<AttributeMetadata> Attributes;
        [DataMember] internal IEnumerable<TypeMetadata> ImplementedInterfaces;
        [DataMember] internal IEnumerable<TypeMetadata> NestedTypes;
        [DataMember] internal IEnumerable<PropertyMetadata> Properties;
        [DataMember] internal IEnumerable<MethodMetadata> Methods;
        [DataMember] internal IEnumerable<MethodMetadata> Constructors;
        [DataMember] internal IEnumerable<EventMetadata> Events;

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

        internal static IEnumerable<AttributeMetadata> EmitAttributes(IEnumerable<Attribute> attributes)
        {
            attributes = attributes.ToList();
            foreach (Attribute attribute in attributes)
            {
                if (TypesDictionary.ReflectedTypes.ContainsKey(attribute.GetType().Name) == false)
                {
                    new TypeMetadata(attribute.GetType());
                }
            }

            return from attribute in attributes
                select new AttributeMetadata(attribute.GetType().Name,
                    EmitReference(attribute.GetType()));
        }

        private static IEnumerable<FieldMetadata> EmitFields(IEnumerable<FieldInfo> fields)
        {
            fields = fields.ToList();
            foreach (FieldInfo field in fields)
            {
                if (TypesDictionary.ReflectedTypes.ContainsKey(field.GetType().Name) == false)
                {
                    new TypeMetadata(field.GetType());
                }
            }

            return from field in fields
                select new FieldMetadata(field.Name, EmitReference(field.FieldType),
                    EmitAttributes(field.GetCustomAttributes()));
        }
        private static IEnumerable<EventMetadata> EmitEvents(IEnumerable<EventInfo> events)
        {
            events = events.ToList();
            foreach (EventInfo singleEvent in events)
            {
                if (TypesDictionary.ReflectedTypes.ContainsKey(singleEvent.GetType().Name) == false)
                {
                    new TypeMetadata(singleEvent.GetType());
                }
            }

            return from singleEvent in events
                select new EventMetadata(singleEvent);
        }

        private TypeMetadata EmitDeclaringType(Type declaringType)
        {
            if (declaringType == null)
                return null;
            return EmitReference(declaringType);
        }

        private IEnumerable<TypeMetadata> EmitNestedTypes(IEnumerable<Type> nestedTypes)
        {
            return from type in nestedTypes
                where type.GetVisible()
                select new TypeMetadata(type);
        }

        private IEnumerable<TypeMetadata> EmitImplements(IEnumerable<Type> interfaces)
        {
            return from currentInterface in interfaces
                select EmitReference(currentInterface);
        }

        private static TypeKind GetTypeKind(Type type)
        {
            return type.IsEnum ? TypeKind.EnumType :
                type.IsValueType ? TypeKind.StructType :
                type.IsInterface ? TypeKind.InterfaceType :
                TypeKind.ClassType;
        }

        internal static Tuple<AccessLevel, SealedEnum, AbstractEnum> EmitModifiers(Type type)
        {
            AccessLevel access = AccessLevel.IsPrivate;
            if (type.IsPublic)
                access = AccessLevel.IsPublic;
            else if (type.IsNestedPublic)
                access = AccessLevel.IsPublic;
            else if (type.IsNestedFamily)
                access = AccessLevel.IsProtected;
            else if (type.IsNestedFamANDAssem)
                access = AccessLevel.IsProtectedInternal;

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