using System.Collections.Generic;
using ModelBase;
using static DataBaseSerializationSurrogates.CollectionOriginalTypeAccessor;
using static DataBaseSerializationSurrogates.CollectionTypeAccessor;

namespace DataBaseSerializationSurrogates.MetadataSurrogates
{
    public class EventMetadataSurrogate 
    {
        #region Constructors

        public EventMetadataSurrogate()
        {
        }

        public EventMetadataSurrogate( EventMetadataBase eventMetadata )
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
            EventAttributes = GetTypesMetadata( eventMetadata.EventAttributes );
        }
        

        #endregion

        #region Properties

        public int? EventId { get; set; }
        public string Name { get; set; }
        public TypeMetadataSurrogate TypeMetadata { get; set; }
        public MethodMetadataSurrogate AddMethodMetadata { get; set; }
        public MethodMetadataSurrogate RaiseMethodMetadata { get; set; }
        public MethodMetadataSurrogate RemoveMethodMetadata { get; set; }
        public bool Multicast { get; set; }
        public ICollection<TypeMetadataSurrogate> EventAttributes { get; set; }

        #endregion

        #region Navigation Properties

        public int? TypeForeignId { get; set; }
        public int? AddMethodId { get; set; }
        public int? RaiseMethodId { get; set; }
        public int? RemoveMethodId { get; set; }
        public ICollection<TypeMetadataSurrogate> TypeEventMetadataSurrogate { get; set; }

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