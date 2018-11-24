using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class FieldMetadataSurrogate
    {
        #region Constructor

        public FieldMetadataSurrogate( FieldMetadata fieldMetadata )
        {
            Name = fieldMetadata.Name;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( fieldMetadata.TypeMetadata );
            IsStatic = fieldMetadata.IsStatic;
            FieldAttributes = GetTypesMetadata( fieldMetadata.FieldAttributes );
        }

        #endregion

        private IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            List<TypeMetadataSurrogate> typeMetadatasSurrogate = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                typeMetadatasSurrogate.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return typeMetadatasSurrogate;
        }

        #region Properties

        [Key]
        public string Name { get; set; }

        public TypeMetadataSurrogate TypeMetadata { get; set; }

        public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }

        public StaticEnum IsStatic { get; set; }

        public IEnumerable<TypeMetadataSurrogate> FieldAttributes { get; set; }

        #endregion

        public FieldMetadata GetOriginalFieldMetadata()
        {
            return new FieldMetadata()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Modifiers = Modifiers,
                IsStatic = IsStatic,
                FieldAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( FieldAttributes )
            };
        }
    }
}