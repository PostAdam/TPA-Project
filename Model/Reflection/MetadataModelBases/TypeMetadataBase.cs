using System;
using System.Collections.Generic;
using System.Linq;
using Model.Reflection.Enums;
using Model.Reflection.NewMetadataModels;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class TypeMetadataBase
    {
        public abstract string TypeName { get; internal set; }
        public abstract string NamespaceName { get; internal set; }
        public abstract TypeMetadataBase BaseType { get; internal set; }
        public abstract TypeMetadataBase DeclaringType { get; internal set; }
        public abstract TypeKind TypeKind { get; internal set; }
        public abstract Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers { get; internal set; }
        public abstract IEnumerable<FieldMetadataBase> Fields { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> GenericArguments { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> Attributes { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> ImplementedInterfaces { get; internal set; }
        public abstract IEnumerable<TypeMetadataBase> NestedTypes { get; internal set; }
        public abstract IEnumerable<PropertyMetadataBase> Properties { get; internal set; }
        public abstract IEnumerable<MethodMetadataBase> Methods { get; internal set; }
        public abstract IEnumerable<MethodMetadataBase> Constructors { get; internal set; }
        public abstract IEnumerable<EventMetadataBase> Events { get; internal set; }
        public abstract string FullName { get; internal set; }

        #region Emit API

        internal static IEnumerable<TypeMetadataBase> EmitTypes( IEnumerable<Type> types )
        {
            return from type in types select EmitType( type );
        }

        internal static TypeMetadataBase EmitType( Type type )
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

            return ReflectedTypes[type.FullName ?? type.Namespace + " . " + type.Name];
        }

        internal static IEnumerable<TypeMetadataBase> EmitAttributes( IEnumerable<Attribute> attributes )
        {
            if ( attributes == null )
                return null;
            IEnumerable<Type> attributesTypes = from attribute in attributes select attribute.GetType();
            return EmitTypes( attributesTypes );
        }

        #endregion

        private static readonly NewMetadataModels.ReflectedTypes ReflectedTypes = NewMetadataModels.ReflectedTypes.Instance;
    }
}