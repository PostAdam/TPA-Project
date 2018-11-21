using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    [DataContract( IsReference = true, Name = "EventMetadata" )]
    public class EventMetadataSurrogate
    {
        public EventMetadataSurrogate( EventMetadata eventMetadata )
        {
            Name = Name;
            TypeMetadata = eventMetadata.TypeMetadata;
            AddMethodMetadata = eventMetadata.AddMethodMetadata;
            RaiseMethodMetadata = eventMetadata.RaiseMethodMetadata;
            RemoveMethodMetadata = eventMetadata.RemoveMethodMetadata;
            Multicast = eventMetadata.Multicast;
            EventAttributes = eventMetadata.EventAttributes;
        }

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeMetadata TypeMetadata { get; set; }

        [DataMember]
        public MethodMetadata AddMethodMetadata { get; set; }

        [DataMember]
        public MethodMetadata RaiseMethodMetadata { get; set; }

        [DataMember]
        public MethodMetadata RemoveMethodMetadata { get; set; }

        [DataMember]
        public bool Multicast { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadata> EventAttributes { get; set; }

        #endregion
    }
}