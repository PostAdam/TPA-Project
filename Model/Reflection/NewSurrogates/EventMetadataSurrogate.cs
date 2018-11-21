﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    [DataContract( IsReference = true, Name = "EventMetadata" )]
    public class EventMetadataSurrogate
    {
        private readonly ReproducedTypes _reproducedTypes = ReproducedTypes.Instance;

        public EventMetadataSurrogate( EventMetadata eventMetadata )
        {
            Name = Name;
            TypeMetadata = _reproducedTypes.GetType( eventMetadata.TypeMetadata )/*new TypeMetadata( eventMetadata.TypeMetadata )*/;
            AddMethodMetadata = new MethodMetadataSurrogate( eventMetadata.AddMethodMetadata );
            RaiseMethodMetadata = new MethodMetadataSurrogate( eventMetadata.RaiseMethodMetadata );
            RemoveMethodMetadata = new MethodMetadataSurrogate( eventMetadata.RemoveMethodMetadata );
            Multicast = eventMetadata.Multicast;
            EventAttributes = CollectionTypeAccessor.GetTypesMetadata( eventMetadata.EventAttributes );
        }

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

        public EventMetadata GetOryginalEventMetadata()
        {
            return new EventMetadata()
            {
                Name = Name,
                TypeMetadata = TypeMetadata.GetOryginalTypeMetadata(),
                AddMethodMetadata = AddMethodMetadata.GetOryginalMethodMetadata(),
                RaiseMethodMetadata = RaiseMethodMetadata.GetOryginalMethodMetadata(),
                RemoveMethodMetadata = RemoveMethodMetadata.GetOryginalMethodMetadata(),
                Multicast = Multicast,
                EventAttributes = CollectionOryginalTypeAccessor.GetOryginalTypesMetadata( EventAttributes )
            };
        }
    }
}