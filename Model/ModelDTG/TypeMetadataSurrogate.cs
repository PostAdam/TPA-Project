using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class TypeMetadataSurrogate
    {
        #region Constructors

        public TypeMetadataSurrogate(TypeMetadataBase typeMetadata)
        {
            TypeName = typeMetadata.TypeName;
            NamespaceName = typeMetadata.NamespaceName;
            FullName = typeMetadata.FullName ?? typeMetadata.NamespaceName + "." + typeMetadata.TypeName;

            ReproducedSurrogateTypes.Add(FullName, this);

            if (typeMetadata.BaseType != null)
            {
                BaseType = new TypeMetadataSurrogate(typeMetadata.BaseType.TypeName,
                    typeMetadata.BaseType.NamespaceName);
            }

            if (typeMetadata.DeclaringType != null)
            {
                DeclaringType = new TypeMetadataSurrogate(typeMetadata.DeclaringType.TypeName,
                    typeMetadata.DeclaringType.NamespaceName);
            }

            TypeKind = typeMetadata.TypeKind;
            Modifiers = typeMetadata.Modifiers;
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

        private TypeMetadataSurrogate(string typeName, string namespaceName)
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
        public IEnumerable<FieldMetadataSurrogate> Fields { get; set; }
        public IEnumerable<TypeMetadataSurrogate> GenericArguments { get; set; }
        public IEnumerable<TypeMetadataSurrogate> Attributes { get; set; }
        public IEnumerable<TypeMetadataSurrogate> ImplementedInterfaces { get; set; }
        public IEnumerable<TypeMetadataSurrogate> NestedTypes { get; set; }
        public IEnumerable<PropertyMetadataSurrogate> Properties { get; set; }
        public IEnumerable<MethodMetadataSurrogate> Methods { get; set; }
        public IEnumerable<MethodMetadataSurrogate> Constructors { get; set; }
        public IEnumerable<EventMetadataSurrogate> Events { get; set; }
        public string FullName { get; set; }

        #endregion

        #region Emiters

        public static TypeMetadataSurrogate EmitSurrogateTypeMetadata(TypeMetadataBase typeMetadata)
        {
            if (typeMetadata == null)
            {
                return null;
            }

            string typeId = typeMetadata.FullName ?? typeMetadata.NamespaceName + "." + typeMetadata.TypeName;
            if (!ReproducedSurrogateTypes.ContainsKey(typeId))
            {
                new TypeMetadataSurrogate(typeMetadata);
            }

            return ReproducedSurrogateTypes[typeId];
        }

        public TypeMetadataBase EmitOriginalTypeMetadata()
        {
            string typeId = FullName;
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
            typeMetadata.TypeKind = TypeKind;
            typeMetadata.Modifiers = Modifiers;
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