using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
{
    public class FieldMetadata : FieldMetadataBase
    {
        #region Constructors

        public FieldMetadata()
        {
        }

        internal FieldMetadata( FieldInfo fieldInfo )
        {
            Name = fieldInfo.Name;
            TypeMetadata = TypeMetadataBase.EmitType( fieldInfo.FieldType );
            FieldAttributes = TypeMetadataBase.EmitAttributes( fieldInfo.GetCustomAttributes() );
            Modifiers = GetModifier( fieldInfo );
            IsStatic = fieldInfo.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;
        }

        #endregion

        #region Properties

        public override string Name { get; set; }
        public override TypeMetadataBase TypeMetadata { get; set; }
        public override Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public override StaticEnum IsStatic { get; set; }
        public override IEnumerable<TypeMetadataBase> FieldAttributes { get; set; }

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