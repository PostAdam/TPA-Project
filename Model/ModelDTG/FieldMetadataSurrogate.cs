using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;

namespace Model.ModelDTG
{
    public class FieldMetadataSurrogate
    {
        #region Constructor

        public FieldMetadataSurrogate( FieldMetadataBase fieldMetadata )
        {
            Name = fieldMetadata.Name;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( fieldMetadata.TypeMetadata );
            IsStatic = fieldMetadata.IsStatic;
            FieldAttributes = GetTypesMetadata( fieldMetadata.FieldAttributes );
            Modifiers = fieldMetadata.Modifiers;
        }

        #endregion

        private IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            List<TypeMetadataSurrogate> typeMetadatasSurrogate = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadataBase typeMetadata in types )
            {
                typeMetadatasSurrogate.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return typeMetadatasSurrogate;
        }

        #region Properties

        public string Name { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public StaticEnum IsStatic { get; set; }
        public IEnumerable<TypeMetadataSurrogate> FieldAttributes { get; set; }

        #endregion

        public FieldMetadataBase GetOriginalFieldMetadata()
        {
            return new FieldMetadataBase()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Modifiers = Modifiers,
                IsStatic = IsStatic,
                FieldAttributes = GetOriginalTypesMetadata( FieldAttributes )
            };
        }
    }
}