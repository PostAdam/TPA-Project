using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.Surrogates
{
    [DataContract( IsReference = true )]
    public class MethodMetadataSurrogate : MethodMetadataBase
    {
        private readonly MethodMetadataBase _realMethodMetadata;// TODO: change to MethodMetadata type

        #region Constructor

        public MethodMetadataSurrogate( MethodMetadataBase realMethodMetadata )
        {
            //            _realMethodMetadata = realMethodMetadata; // TODO: need to change MethodMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public override string Name
        {
            get => _realMethodMetadata.Name;
            set => _realMethodMetadata.Name = value;
        }
        [DataMember]
        public override bool Extension
        {
            get => _realMethodMetadata.Extension;
            set => _realMethodMetadata.Extension = value;
        }
        [DataMember]
        public override TypeMetadataBase ReturnType
        {
            get => _realMethodMetadata.ReturnType;
            set => _realMethodMetadata.ReturnType = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> MethodAttributes
        {
            get => _realMethodMetadata.MethodAttributes;
            set => _realMethodMetadata.MethodAttributes = value;
        }
        [DataMember]
        public override IEnumerable<ParameterMetadataBase> Parameters
        {
            get => _realMethodMetadata.Parameters;
            set => _realMethodMetadata.Parameters = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> GenericArguments
        {
            get => _realMethodMetadata.GenericArguments;
            set => _realMethodMetadata.GenericArguments = value;
        }
        [DataMember]
        public override Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers
        {
            get => _realMethodMetadata.Modifiers;
            set => _realMethodMetadata.Modifiers = value;
        }

        #endregion
    }
}