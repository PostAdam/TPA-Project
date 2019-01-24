using System.Collections.Generic;
using System.Runtime.Serialization;
using ModelBase;
using static XmlSerializationSurrogates.CollectionOriginalTypeAccessor;
using static XmlSerializationSurrogates.CollectionTypeAccessor;

namespace XmlSerializationSurrogates.MetadataSurrogates
{
    [DataContract( IsReference = true, Name = "EventMetadata" )]
    public class EventMetadataSurrogate
    {
        #region Constructor

        public EventMetadataSurrogate( EventMetadataBase eventMetadata )
        {
            Name = eventMetadata.Name;
            TypeMetadata = TypeMetadataSurrogate.EmitSurrogateTypeMetadata( eventMetadata.TypeMetadata );
            AddMethodMetadata = eventMetadata.AddMethodMetadata == null
                ? null
                : new MethodMetadataSurrogate( eventMetadata.AddMethodMetadata );
            RaiseMethodMetadata = eventMetadata.RaiseMethodMetadata == null
                ? null
                : new MethodMetadataSurrogate( eventMetadata.RaiseMethodMetadata );
            RemoveMethodMetadata = eventMetadata.RemoveMethodMetadata == null
                ? null
                : new MethodMetadataSurrogate( eventMetadata.RemoveMethodMetadata );
            Multicast = eventMetadata.Multicast;
            EventAttributes = GetTypesMetadata( eventMetadata.EventAttributes );
        }

        #endregion

        #region Properties

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public TypeMetadataSurrogate TypeMetadata { get; set; }

        [DataMember]
        public MethodMetadataSurrogate AddMethodMetadata { get; set; }

        [DataMember]
        public MethodMetadataSurrogate RaiseMethodMetadata { get; set; }

        [DataMember]
        public MethodMetadataSurrogate RemoveMethodMetadata { get; set; }

        [DataMember]
        public bool Multicast { get; set; }

        [DataMember]
        public IEnumerable<TypeMetadataSurrogate> EventAttributes { get; set; }

        #endregion

        public EventMetadataBase GetOriginalEventMetadata()
        {
            return new EventMetadataBase()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                AddMethodMetadata = AddMethodMetadata?.GetOriginalMethodMetadata(),
                RaiseMethodMetadata = RaiseMethodMetadata?.GetOriginalMethodMetadata(),
                RemoveMethodMetadata = RemoveMethodMetadata?.GetOriginalMethodMetadata(),
                Multicast = Multicast,
                EventAttributes = GetOriginalTypesMetadata( EventAttributes )
            };
        }
    }
}