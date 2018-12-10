using System.Collections.Generic;
using Model.Reflection.MetadataModels;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class EventMetadataSurrogate
    {
        #region Constructor

        public EventMetadataSurrogate( EventMetadata eventMetadata )
        {
            Name = Name;
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
            EventAttributes = GetTypesMetadata( eventMetadata.EventAttributes ) as ICollection<TypeMetadataSurrogate>;
        }

        #endregion

        #region Properties

        public int EventId { get; set; }
        public string Name { get; set; }
        public int TypeMetadataId { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public int AddMethodMetadataId { get; set; }
        public MethodMetadataSurrogate AddMethodMetadata { get; set; }
        public int RaiseMethodMetadataId { get; set; }
        public MethodMetadataSurrogate RaiseMethodMetadata { get; set; }
        public int RemoveMethodMetadataId { get; set; }
        public MethodMetadataSurrogate RemoveMethodMetadata { get; set; }
        public bool Multicast { get; set; }
        public ICollection<TypeMetadataSurrogate> EventAttributes { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<TypeMetadataSurrogate> TypeEventMetadataSurrogate { get; set; }

        #endregion

        public EventMetadata GetOriginalEventMetadata()
        {
            return new EventMetadata()
            {
                Name = Name,
                TypeMetadata = TypeMetadata?.EmitOriginalTypeMetadata(),
                AddMethodMetadata = AddMethodMetadata?.GetOriginalMethodMetadata(),
                RaiseMethodMetadata = RaiseMethodMetadata?.GetOriginalMethodMetadata(),
                RemoveMethodMetadata = RemoveMethodMetadata?.GetOriginalMethodMetadata(),
                Multicast = Multicast,
                EventAttributes = CollectionOriginalTypeAccessor.GetOriginalTypesMetadata( EventAttributes )
            };
        }
    }
}