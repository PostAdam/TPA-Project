using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
{
    public class FieldMetadata
    {
        #region Constructors

        public FieldMetadata()
        {
        }

        internal FieldMetadata( FieldInfo fieldInfo )
        {
            Name = fieldInfo.Name;
            TypeMetadata = TypeMetadata.EmitType( fieldInfo.FieldType );
            FieldAttributes = TypeMetadata.EmitAttributes( fieldInfo.GetCustomAttributes() );
            Modifiers = GetModifier( fieldInfo );
            IsStatic = fieldInfo.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
        public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public StaticEnum IsStatic { get; set; }
        public IEnumerable<TypeMetadata> FieldAttributes { get; set; }

        #endregion

        internal static Tuple<AccessLevel, StaticEnum> GetModifier( FieldInfo fieldInfo )
        {
            AccessLevel access = AccessLevel.Private;
            if ( fieldInfo.IsPublic )
                access = AccessLevel.Public;
            else if ( fieldInfo.IsFamilyOrAssembly )
                access = AccessLevel.Public;
            else if ( fieldInfo.IsFamily )
                access = AccessLevel.Protected;
            else if ( fieldInfo.IsAssembly )
                access = AccessLevel.Internal;

            StaticEnum _static = StaticEnum.NotStatic;
            if ( fieldInfo.IsStatic )
                _static = StaticEnum.Static;

            return new Tuple<AccessLevel, StaticEnum>( access, _static );
        }
    }
}