using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Model.Reflection.MetadataModels;

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
            EventAttributes = CollectionTypeAccessor.GetTypesMetadata( eventMetadata.EventAttributes );
        }

        #endregion

        #region Properties

        [Key]
        public string Name { get; set; }

        public TypeMetadataSurrogate TypeMetadata { get; set; }

        public MethodMetadataSurrogate AddMethodMetadata { get; set; }

        public MethodMetadataSurrogate RaiseMethodMetadata { get; set; }

        public MethodMetadataSurrogate RemoveMethodMetadata { get; set; }

        public bool Multicast { get; set; }

        public IEnumerable<TypeMetadataSurrogate> EventAttributes { get; set; }

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