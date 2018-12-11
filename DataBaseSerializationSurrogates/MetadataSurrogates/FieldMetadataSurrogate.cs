using System;
using System.Collections.Generic;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class FieldMetadataSurrogate
    {
        #region Constructors

        public FieldMetadataSurrogate()
        {
        }

        public FieldMetadataSurrogate( FieldMetadata fieldMetadata )
        {
            Name = fieldMetadata.Name;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( fieldMetadata.TypeMetadata );
            IsStatic = fieldMetadata.IsStatic;

            IEnumerable<TypeMetadataSurrogate> fieldAttributes = GetTypesMetadata( fieldMetadata.FieldAttributes );
            FieldAttributes = fieldAttributes == null ? null : new List<TypeMetadataSurrogate>( fieldAttributes );
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

        public int FieldId { get; set; }
        public string Name { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public StaticEnum IsStatic { get; set; }
        public ICollection<TypeMetadataSurrogate> FieldAttributes { get; set; }
        private Tuple<AccessLevel, StaticEnum> _modifiers;

        #region Modifiers

        public AccessLevel AccessLevel
        {
            get => _modifiers.Item1;
            set => _modifiers = new Tuple<AccessLevel, StaticEnum>( value, _modifiers.Item2 );
        }
        public StaticEnum StaticEnum
        {
            get => _modifiers.Item2;
            set => _modifiers = new Tuple<AccessLevel, StaticEnum>( _modifiers.Item1, value );
        }

        #endregion

        #endregion

        #region Navigation Properties

        public int TypeForeignId { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeFieldsSurrogates { get; set; }

        #endregion

        public FieldMetadata GetOriginalFieldMetadata()
        {
            return new FieldMetadata()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Modifiers = _modifiers,
                IsStatic = IsStatic,
                FieldAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( FieldAttributes )
            };
        }
    }
}