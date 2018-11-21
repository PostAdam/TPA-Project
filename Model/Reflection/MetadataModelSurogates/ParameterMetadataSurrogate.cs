using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    [DataContract( IsReference = true, Name = "ParameterMetadata" )]
    public class ParameterMetadataSurrogate
    {
        private readonly ParameterMetadata _realParameterMetadata;

        #region Constructor

        public ParameterMetadataSurrogate()
        {
            _realParameterMetadata = new ParameterMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name
        {
            get => _realParameterMetadata.Name;
            set => _realParameterMetadata.Name = value;
        }
        [DataMember]
        public TypeMetadata TypeMetadata
        {
            get => _realParameterMetadata.TypeMetadata;
            set => _realParameterMetadata.TypeMetadata = value;
        }
        [DataMember]
        public int Position
        {
            get => _realParameterMetadata.Position;
            set => _realParameterMetadata.Position = value;
        }
        [DataMember]
        public ParameterKindEnum Kind
        {
            get => _realParameterMetadata.Kind;
            set => _realParameterMetadata.Kind = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> ParameterAttributes
        {
            get => _realParameterMetadata.ParameterAttributes;
            set => _realParameterMetadata.ParameterAttributes = value;
        }
        [DataMember]
        public string DefaultValue
        {
            get => _realParameterMetadata.DefaultValue;
            set => _realParameterMetadata.DefaultValue = value;
        }

        #endregion
    }
}