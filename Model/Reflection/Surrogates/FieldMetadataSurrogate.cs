using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.Surrogates
{
    [DataContract( IsReference = true )]
    public class FieldMetadataSurrogate : FieldMetadataBase
    {
        private readonly FieldMetadataBase _realFieldMetadata;// TODO: change to FieldMetadata type

        #region Constructor

        public FieldMetadataSurrogate( FieldMetadataBase realFieldMetadata )
        {
            //            _realFieldMetadata = realFieldMetadata; // TODO: need to change FieldMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public override string Name
        {
            get => _realFieldMetadata.Name;
            set => _realFieldMetadata.Name = value;
        }
        [DataMember]
        public override TypeMetadataBase TypeMetadata
        {
            get => _realFieldMetadata.TypeMetadata;
            set => _realFieldMetadata.TypeMetadata = value;
        }
        [DataMember]
        public override Tuple<AccessLevel, StaticEnum> Modifiers
        {
            get => _realFieldMetadata.Modifiers;
            set => _realFieldMetadata.Modifiers = value;
        }
        [DataMember]
        public override StaticEnum IsStatic
        {
            get => _realFieldMetadata.IsStatic;
            set => _realFieldMetadata.IsStatic = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> FieldAttributes
        {
            get => _realFieldMetadata.FieldAttributes;
            set => _realFieldMetadata.FieldAttributes = value;
        }

        #endregion
    }
}