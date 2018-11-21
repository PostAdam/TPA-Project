using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    [DataContract( IsReference = true, Name = "PropertyMetadata" )]
    public class PropertyMetadataSurrogate
    {
        private readonly PropertyMetadata _realPropertyMetadata;

        #region Constructor

        public PropertyMetadataSurrogate()
        {
            _realPropertyMetadata = new PropertyMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name
        {
            get => _realPropertyMetadata.Name;
            set => _realPropertyMetadata.Name = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> PropertyAttributes
        {
            get => _realPropertyMetadata.PropertyAttributes;
            set => _realPropertyMetadata.PropertyAttributes = value;
        }
        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers
        {
            get => _realPropertyMetadata.Modifiers;
            set => _realPropertyMetadata.Modifiers = value;
        }
        [DataMember]
        public TypeMetadata TypeMetadata
        {
            get => _realPropertyMetadata.TypeMetadata;
            set => _realPropertyMetadata.TypeMetadata = value;
        }
        [DataMember]
        public PropertyInfo PropertyInfo
        {
            get => _realPropertyMetadata.PropertyInfo;
            set => _realPropertyMetadata.PropertyInfo = value;
        }
        [DataMember]
        public MethodMetadata Getter
        {
            get => _realPropertyMetadata.Getter;
            set => _realPropertyMetadata.Getter = value;
        }
        [DataMember]
        public MethodMetadata Setter
        {
            get => _realPropertyMetadata.Setter;
            set => _realPropertyMetadata.Setter = value;
        }

        #endregion
    }
}