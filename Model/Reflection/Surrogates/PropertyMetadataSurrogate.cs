using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.Surrogates
{
    [DataContract( IsReference = true )]
    public class PropertyMetadataSurrogate : PropertyMetadataBase
    {
        private readonly PropertyMetadataBase _realPropertyMetadata;// TODO: change to PropertyMetadata type

        #region Constructor

        public PropertyMetadataSurrogate( PropertyMetadataBase realPropertyMetadata )
        {
            //            _realPropertyMetadata = realPropertyMetadata; // TODO: need to change PropertyMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public override string Name
        {
            get => _realPropertyMetadata.Name;
            set => _realPropertyMetadata.Name = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> PropertyAttributes
        {
            get => _realPropertyMetadata.PropertyAttributes;
            set => _realPropertyMetadata.PropertyAttributes = value;
        }
        [DataMember]
        public override Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers
        {
            get => _realPropertyMetadata.Modifiers;
            set => _realPropertyMetadata.Modifiers = value;
        }
        [DataMember]
        public override TypeMetadataBase TypeMetadata
        {
            get => _realPropertyMetadata.TypeMetadata;
            set => _realPropertyMetadata.TypeMetadata = value;
        }
        [DataMember]
        public override PropertyInfo PropertyInfo
        {
            get => _realPropertyMetadata.PropertyInfo;
            set => _realPropertyMetadata.PropertyInfo = value;
        }
        [DataMember]
        public override MethodMetadataBase Getter
        {
            get => _realPropertyMetadata.Getter;
            set => _realPropertyMetadata.Getter = value;
        }
        [DataMember]
        public override MethodMetadataBase Setter
        {
            get => _realPropertyMetadata.Setter;
            set => _realPropertyMetadata.Setter = value;
        }

        #endregion
    }
}