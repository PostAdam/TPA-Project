using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    [DataContract( IsReference = true, Name = "EventMetadata" )]
    public class EventMetadataSurrogate
    {
        private readonly EventMetadata _realEventMetadataBase;

        #region Constructor

        public EventMetadataSurrogate()
        {
            _realEventMetadataBase = new EventMetadata();
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name
        {
            get => _realEventMetadataBase.Name;
            set => _realEventMetadataBase.Name = value;
        }
        [DataMember]
        public TypeMetadata TypeMetadata
        {
            get => _realEventMetadataBase.TypeMetadata;
            set => _realEventMetadataBase.TypeMetadata = value;
        }
        [DataMember]
        public MethodMetadata AddMethodMetadata
        {
            get => _realEventMetadataBase.AddMethodMetadata;
            set => _realEventMetadataBase.AddMethodMetadata = value;
        }
        [DataMember]
        public MethodMetadata RaiseMethodMetadata
        {
            get => _realEventMetadataBase.RaiseMethodMetadata;
            set => _realEventMetadataBase.RaiseMethodMetadata = value;
        }
        [DataMember]
        public MethodMetadata RemoveMethodMetadata
        {
            get => _realEventMetadataBase.RemoveMethodMetadata;
            set => _realEventMetadataBase.RemoveMethodMetadata = value;
        }
        [DataMember]
        public bool Multicast
        {
            get => _realEventMetadataBase.Multicast;
            set => _realEventMetadataBase.Multicast = value;
        }
        [DataMember]
        public IEnumerable<TypeMetadata> EventAttributes
        {
            get => _realEventMetadataBase.EventAttributes;
            set => _realEventMetadataBase.EventAttributes = value;
        }

        #endregion
    }
}