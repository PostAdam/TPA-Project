using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "FieldMetadata" )]
    public class FieldMetadataSurrogate
    {
        public FieldMetadataSurrogate( FieldMetadata fieldMetadata )
        {
            Name = fieldMetadata.Name;
            TypeMetadata = _reproducedTypes.GetType( fieldMetadata.TypeMetadata );
            IsStatic = fieldMetadata.IsStatic;
            FieldAttributes = GetTypesMetadata( fieldMetadata.FieldAttributes );
        }

        private readonly ReproducedTypes _reproducedTypes = ReproducedTypes.Instance;

        private IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            List<TypeMetadataSurrogate> typeMetadatasSurrogate = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                typeMetadatasSurrogate.Add( _reproducedTypes.GetType( typeMetadata ) );
            }

            return typeMetadatasSurrogate;
        }

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeMetadataSurrogate TypeMetadata { get; set; }

        [DataMember]
        public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }

        [DataMember]
        public StaticEnum IsStatic { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> FieldAttributes { get; set; }

        #endregion

        public FieldMetadata GetOryginalFieldMetadata()
        {
            return new FieldMetadata()
            {
                Name = Name,
                TypeMetadata = TypeMetadata.GetOryginalTypeMetadata(),
                Modifiers = Modifiers,
                IsStatic = IsStatic,
                FieldAttributes = CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( FieldAttributes )
            };
        }
    }
}