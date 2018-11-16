using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.Surrogates
{
    [DataContract( IsReference = true )]
    public class EventMetadataSurrogate : EventMetadataBase
    {
        private readonly EventMetadataBase _realEventMetadataBase;// TODO: change to EventMetadata type

        #region Constructor

        public EventMetadataSurrogate( EventMetadataBase realEventMetadata )
        {
            //            _realEventMetadataBase = realEventMetadata; // TODO: need to change EventMetadata structure
        }

        #endregion

        #region Properties

        [DataMember]
        public override string Name
        {
            get => _realEventMetadataBase.Name;
            set => _realEventMetadataBase.Name = value;
        }
        [DataMember]
        public override TypeMetadataBase TypeMetadata
        {
            get => _realEventMetadataBase.TypeMetadata;
            set => _realEventMetadataBase.TypeMetadata = value;
        }
        [DataMember]
        public override MethodMetadataBase AddMethodMetadata
        {
            get => _realEventMetadataBase.AddMethodMetadata;
            set => _realEventMetadataBase.AddMethodMetadata = value;
        }
        [DataMember]
        public override MethodMetadataBase RaiseMethodMetadata
        {
            get => _realEventMetadataBase.RaiseMethodMetadata;
            set => _realEventMetadataBase.RaiseMethodMetadata = value;
        }
        [DataMember]
        public override MethodMetadataBase RemoveMethodMetadata
        {
            get => _realEventMetadataBase.RemoveMethodMetadata;
            set => _realEventMetadataBase.RemoveMethodMetadata = value;
        }
        [DataMember]
        public override bool Multicast
        {
            get => _realEventMetadataBase.Multicast;
            set => _realEventMetadataBase.Multicast = value;
        }
        [DataMember]
        public override IEnumerable<TypeMetadataBase> EventAttributes
        {
            get => _realEventMetadataBase.EventAttributes;
            set => _realEventMetadataBase.EventAttributes = value;
        }

        #endregion
    }
}