using System;
using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.MetadataModels;
using ModelBase;
using Model.Reflection.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;

namespace Model.ModelDTG
{
    public class FieldMetadata
    {
        #region Constructor

        public FieldMetadata()
        {
        }

        internal FieldMetadata( FieldInfo fieldInfo )
        {
            Name = fieldInfo.Name;
            TypeMetadata = TypeReflector.EmitType( fieldInfo.FieldType );
            FieldAttributes = TypeReflector.EmitAttributes( fieldInfo.CustomAttributes );
            Modifiers = FieldReflector.GetModifier( fieldInfo );
            IsStatic = fieldInfo.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;
        }


        public FieldMetadata( FieldMetadataBase fieldMetadata )
        {
            Name = fieldMetadata.Name;
            TypeMetadata = TypeMetadata.EmitSurrogateTypeMetadata( fieldMetadata.TypeMetadata );
            IsStatic = (StaticEnum) fieldMetadata.IsStatic;
            FieldAttributes = GetTypesMetadata( fieldMetadata.FieldAttributes );
            Modifiers = Tuple.Create( ( AccessLevel ) fieldMetadata.Modifiers.Item1,
                ( StaticEnum ) fieldMetadata.Modifiers.Item2);
        }

        #endregion

        private IEnumerable<TypeMetadata> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            List<TypeMetadata> typeMetadatasSurrogate = new List<TypeMetadata>();
            foreach ( TypeMetadataBase typeMetadata in types )
            {
                typeMetadatasSurrogate.Add( ModelDTG.TypeMetadata.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return typeMetadatasSurrogate;
        }

        #region Properties

        public string Name { get; set; }
        public TypeMetadata TypeMetadata { get; set; }
        public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public StaticEnum IsStatic { get; set; }
        public IEnumerable<TypeMetadata> FieldAttributes { get; set; }

        #endregion

        public FieldMetadataBase GetOriginalFieldMetadata()
        {
            return new FieldMetadataBase()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Modifiers = Tuple.Create( ( ModelBase.Enums.AccessLevel ) Modifiers.Item1,
                    ( ModelBase.Enums.StaticEnum ) Modifiers.Item2 ),
                IsStatic = (ModelBase.Enums.StaticEnum) IsStatic,
                FieldAttributes = GetOriginalTypesMetadata( FieldAttributes )
            };
        }
    }
}