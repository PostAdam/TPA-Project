using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    [DataContract( IsReference = true, Name = "FieldMetadata" )]
    public class FieldMetadataSurrogate
    {
        private readonly FieldMetadata _realFieldMetadata;

        #region Constructor

        public FieldMetadataSurrogate()
        {
            _realFieldMetadata = new FieldMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name
        {
            get => _realFieldMetadata.Name;
            set => _realFieldMetadata.Name = value;
        }
        [DataMember]
        public TypeMetadata TypeMetadata
        {
            get => _realFieldMetadata.TypeMetadata;
            set => _realFieldMetadata.TypeMetadata = value;
        }
        [DataMember]
        public Tuple<AccessLevel, StaticEnum> Modifiers
        {
            get => _realFieldMetadata.Modifiers;
            set => _realFieldMetadata.Modifiers = value;
        }
        [DataMember]
        public StaticEnum IsStatic
        {
            get => _realFieldMetadata.IsStatic;
            set => _realFieldMetadata.IsStatic = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> FieldAttributes
        {
            get => _realFieldMetadata.FieldAttributes;
            set => _realFieldMetadata.FieldAttributes = value;
        }

        #endregion
    }
}