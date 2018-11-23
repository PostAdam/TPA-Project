using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static Model.Reflection.NewSurrogates.CollectionOryginalTypeAccessor;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "FieldMetadata" )]
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
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                Modifiers = Modifiers,
                IsStatic = IsStatic,
                FieldAttributes = GetOryginalTypesMetadata( FieldAttributes )
            };
        }
    }
}