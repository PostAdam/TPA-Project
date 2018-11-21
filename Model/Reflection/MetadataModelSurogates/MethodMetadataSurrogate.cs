using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    [DataContract( IsReference = true, Name = "MethodMetadata" )]
    public class MethodMetadataSurrogate
    {
        private readonly MethodMetadata _realMethodMetadata;

        #region Constructor

        public MethodMetadataSurrogate()
        {
            _realMethodMetadata = new MethodMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name
        {
            get => _realMethodMetadata.Name;
            set => _realMethodMetadata.Name = value;
        }
        [DataMember]
        public bool Extension
        {
            get => _realMethodMetadata.Extension;
            set => _realMethodMetadata.Extension = value;
        }
        [DataMember]
        public TypeMetadata ReturnType
        {
            get => _realMethodMetadata.ReturnType;
            set => _realMethodMetadata.ReturnType = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> MethodAttributes
        {
            get => _realMethodMetadata.MethodAttributes;
            set => _realMethodMetadata.MethodAttributes = value;
        }
        [DataMember]
        public IEnumerable<ParameterMetadata> Parameters
        {
            get => _realMethodMetadata.Parameters;
            set => _realMethodMetadata.Parameters = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> GenericArguments
        {
            get => _realMethodMetadata.GenericArguments;
            set => _realMethodMetadata.GenericArguments = value;
        }
        [DataMember]
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers
        {
            get => _realMethodMetadata.Modifiers;
            set => _realMethodMetadata.Modifiers = value;
        }

        #endregion
    }
}