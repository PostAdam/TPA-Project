using System;
using System.Collections.Generic;
using ModelBase;
using ModelBase.Enums;
using static DataBaseSerializationSurrogates.CollectionOriginalTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class FieldMetadataSurrogate
    {
        #region Constructors

        public FieldMetadataSurrogate()
        {
        }

        public FieldMetadataSurrogate( FieldMetadataBase fieldMetadata )
        {
            Name = fieldMetadata.Name;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( fieldMetadata.TypeMetadata );
            IsStatic = fieldMetadata.IsStatic;
            FieldAttributes = GetTypesMetadata( fieldMetadata.FieldAttributes );
            _modifiers = fieldMetadata.Modifiers;
        }

        #endregion

        private ICollection<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            List<TypeMetadataSurrogate> typeMetadatasSurrogate = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadataBase typeMetadata in types )
            {
                typeMetadatasSurrogate.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return typeMetadatasSurrogate;
        }

        #region Properties

        public int? FieldId { get; set; }
        public string Name { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public StaticEnum IsStatic { get; set; }
        public ICollection<TypeMetadataSurrogate> FieldAttributes { get; set; }
        private Tuple<AccessLevel, StaticEnum> _modifiers;

        #region Modifiers

        public AccessLevel? AccessLevel
        {
            get => _modifiers?.Item1;
            set
            {
                if ( value != null ) _modifiers = new Tuple<AccessLevel, StaticEnum>( 
                    value.Value,
                    _modifiers?.Item2 ?? ModelBase.Enums.StaticEnum.NotStatic );
            }
        }

        public StaticEnum? StaticEnum
        {
            get => _modifiers?.Item2;
            set
            {
                if ( value != null ) _modifiers = new Tuple<AccessLevel, StaticEnum>(
                    _modifiers?.Item1 ?? ModelBase.Enums.AccessLevel.Public,
                    value.Value );
            }
        }

        #endregion

        #endregion

        #region Navigation Properties

        public int? TypeForeignId { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeFieldsSurrogates { get; set; }

        #endregion

        public FieldMetadataBase GetOriginalFieldMetadata()
        {
            return new FieldMetadataBase()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Modifiers = _modifiers,
                IsStatic = IsStatic,
                FieldAttributes = GetOriginalTypesMetadata( FieldAttributes )
            };
        }
    }
}