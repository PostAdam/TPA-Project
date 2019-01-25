using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.MetadataModels;
using ModelBase;
using Model.Reflection.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class TypeMetadata
    {
        #region Constructors

        public TypeMetadata()
        {
        }

        internal TypeMetadata(Type type)
        {
            // Infos
            TypeName = type.Name;
            NamespaceName = type.Namespace;
            FullName = type.FullName ?? type.Namespace + "." + type.Name;
            Modifiers = TypeReflector.EmitModifiers(type);
            TypeKind = TypeReflector.GetTypeKind(type);

            // Types
            BaseType = TypeReflector.EmitExtends(type.GetTypeInfo().BaseType);
            ImplementedInterfaces = TypeReflector.EmitTypes(type.GetInterfaces());
            DeclaringType = TypeReflector.EmitType(type.DeclaringType);
            NestedTypes = TypeReflector.EmitTypes(type.GetNestedTypes(AllAccessLevels));
            Attributes = TypeReflector.EmitAttributes(type.CustomAttributes);
            Fields = TypeReflector.EmitFields(type.GetFields(AllAccessLevels));
            Events = TypeReflector.EmitEvents(type.GetEvents(AllAccessLevels));
            GenericArguments = type.IsGenericTypeDefinition
                ? TypeReflector.EmitGenericArguments(type.GetGenericArguments())
                : null;
            Properties = TypeReflector.EmitProperties(type.GetProperties(AllAccessLevels));

            // Methods
            Constructors = MethodMetadata.EmitMethods(type.GetConstructors(AllAccessLevels));
            Methods = MethodMetadata.EmitMethods(type.GetMethods(AllAccessLevels));
        }

        public TypeMetadata(TypeMetadataBase typeMetadata)
        {
            TypeName = typeMetadata.TypeName;
            NamespaceName = typeMetadata.NamespaceName;
            FullName = typeMetadata.FullName ?? typeMetadata.NamespaceName + "." + typeMetadata.TypeName;

            ReproducedSurrogateTypes.Add(FullName, this);

            if (typeMetadata.BaseType != null)
            {
                BaseType = new TypeMetadata(typeMetadata.BaseType.TypeName,
                    typeMetadata.BaseType.NamespaceName);
            }

            if (typeMetadata.DeclaringType != null)
            {
                DeclaringType = new TypeMetadata(typeMetadata.DeclaringType.TypeName,
                    typeMetadata.DeclaringType.NamespaceName);
            }

            TypeKind = (TypeKind)typeMetadata.TypeKind;
            Modifiers = typeMetadata.Modifiers == null ? null : Tuple.Create((AccessLevel)typeMetadata.Modifiers.Item1,
                (SealedEnum) typeMetadata.Modifiers.Item2, (AbstractEnum)typeMetadata.Modifiers.Item3);
            Fields = GetFieldsMetadata(typeMetadata.Fields);

            GenericArguments = GetTypesMetadata(typeMetadata.GenericArguments);
            Attributes = GetTypesMetadata(typeMetadata.Attributes);
            ImplementedInterfaces = GetTypesMetadata(typeMetadata.ImplementedInterfaces);
            NestedTypes = GetTypesMetadata(typeMetadata.NestedTypes);
            Properties = GetPropertiesMetadata(typeMetadata.Properties);
            Methods = GetMethodsMetadata(typeMetadata.Methods);
            Constructors = GetMethodsMetadata(typeMetadata.Constructors);
            Events = GetEventsMetadata(typeMetadata.Events);
        }

        public TypeMetadata(string typeName, string namespaceName)
        {
            TypeName = typeName;
            NamespaceName = namespaceName;
            FullName = namespaceName + "." + typeName;
        }

        public TypeMetadata(string typeName, string namespaceName,
            IEnumerable<TypeMetadata> genericArguments) :
            this(typeName, namespaceName)
        {
            GenericArguments = genericArguments;
        }

        #endregion


        #region Properties

        public string TypeName { get; set; }
        public string NamespaceName { get; set; }
        public TypeMetadata BaseType { get; set; }
        public TypeMetadata DeclaringType { get; set; }
        public TypeKind TypeKind { get; set; }
        public Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; set; }
        public IEnumerable<FieldMetadata> Fields { get; set; }
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        public IEnumerable<TypeMetadata> Attributes { get; set; }
        public IEnumerable<TypeMetadata> ImplementedInterfaces { get; set; }
        public IEnumerable<TypeMetadata> NestedTypes { get; set; }
        public IEnumerable<PropertyMetadata> Properties { get; set; }
        public IEnumerable<MethodMetadata> Methods { get; set; }
        public IEnumerable<MethodMetadata> Constructors { get; set; }
        public IEnumerable<EventMetadata> Events { get; set; }
        public string FullName { get; set; }

        const BindingFlags AllAccessLevels = BindingFlags.NonPublic
                                             | BindingFlags.DeclaredOnly
                                             | BindingFlags.Public
                                             | BindingFlags.Static
                                             | BindingFlags.Instance;

        #endregion

        #region Emiters

        public static TypeMetadata EmitSurrogateTypeMetadata(TypeMetadataBase typeMetadata)
        {
            if (typeMetadata == null)
            {
                return null;
            }

            string typeId = typeMetadata.FullName ?? typeMetadata.NamespaceName + "." + typeMetadata.TypeName;
            if (!ReproducedSurrogateTypes.ContainsKey(typeId))
            {
                new TypeMetadata(typeMetadata);
            }

            return ReproducedSurrogateTypes[typeId];
        }

        public TypeMetadataBase EmitOriginalTypeMetadata()
        {
            string typeId = FullName;
            if (typeId == null) return null;
            if (!ReproducedOriginalTypes.ContainsKey(typeId))
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
            ReproducedOriginalTypes.Add(FullName, typeMetadata);
            PopulateTypeMetadataWithData(typeMetadata);
        }

        private void PopulateTypeMetadataWithData(TypeMetadataBase typeMetadata)
        {
            typeMetadata.TypeName = TypeName;
            typeMetadata.NamespaceName = NamespaceName;
            typeMetadata.BaseType = BaseType?.EmitOriginalTypeMetadata();
            typeMetadata.DeclaringType = DeclaringType?.EmitOriginalTypeMetadata();
            typeMetadata.TypeKind = (ModelBase.Enums.TypeKind) TypeKind;
            typeMetadata.Modifiers = Modifiers != null ? Tuple.Create( ( ModelBase.Enums.AccessLevel ) Modifiers.Item1,
                ( ModelBase.Enums.SealedEnum ) Modifiers.Item2, ( ModelBase.Enums.AbstractEnum ) Modifiers.Item3 ) : null;
            typeMetadata.Fields = GetOriginalFieldsMetadata(Fields);
            typeMetadata.GenericArguments = GetOriginalTypesMetadata(GenericArguments);
            typeMetadata.Attributes = GetOriginalTypesMetadata(Attributes);
            typeMetadata.ImplementedInterfaces = GetOriginalTypesMetadata(ImplementedInterfaces);
            typeMetadata.NestedTypes = GetOriginalTypesMetadata(NestedTypes);
            typeMetadata.Properties = GetOriginalPropertiesMetadata(Properties);
            typeMetadata.Methods = GetOriginalMethodsMetadata(Methods);
            typeMetadata.Constructors = GetOriginalMethodsMetadata(Constructors);
            typeMetadata.Events = GetOriginalEventsMetadata(Events);
            typeMetadata.FullName = FullName;
        }

        #endregion
    }
}