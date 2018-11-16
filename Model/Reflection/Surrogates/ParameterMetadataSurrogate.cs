using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.Surrogates
{
    [DataContract( IsReference = true )]
    public class ParameterMetadataSurrogate : ParameterMetadataBase
    {
        private readonly ParameterMetadataBase _realParameterMetadata;// TODO: change to ParameterMetadata type

        #region Constructor

        public ParameterMetadataSurrogate( ParameterMetadataBase realParameterMetadata )
        {
            //            _realParameterMetadata = realParameterMetadata; // TODO: need to change ParameterMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public override string Name
        {
            get => _realParameterMetadata.Name;
            set => _realParameterMetadata.Name = value;
        }
        [DataMember]
        public override TypeMetadata TypeMetadata
        {
            get => _realParameterMetadata.TypeMetadata;
            set => _realParameterMetadata.TypeMetadata = value;
        }
        [DataMember]
        public override int Position
        {
            get => _realParameterMetadata.Position;
            set => _realParameterMetadata.Position = value;
        }
        [DataMember]
        public override ParameterKindEnum Kind
        {
            get => _realParameterMetadata.Kind;
            set => _realParameterMetadata.Kind = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> ParameterAttributes
        {
            get => _realParameterMetadata.ParameterAttributes;
            set => _realParameterMetadata.ParameterAttributes = value;
        }
        [DataMember]
        public override string DefaultValue
        {
            get => _realParameterMetadata.DefaultValue;
            set => _realParameterMetadata.DefaultValue = value;
        }

        #endregion
    }
}