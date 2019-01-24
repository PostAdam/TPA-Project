using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.MetadataModels;
using ModelBase;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class EventMetadata
    {
        #region Constructor
        public EventMetadata()
        { }

        internal EventMetadata( EventInfo eventInfo )
        {
            Name = eventInfo.Name;
            TypeMetadata = TypeReflector.EmitType( eventInfo.EventHandlerType );
            EventAttributes = TypeReflector.EmitAttributes( eventInfo.CustomAttributes );
            AddMethodMetadata = MethodMetadata.EmitMethod( eventInfo.AddMethod );
            RaiseMethodMetadata = MethodMetadata.EmitMethod( eventInfo.RaiseMethod );
            RemoveMethodMetadata = MethodMetadata.EmitMethod( eventInfo.RemoveMethod );
            Multicast = eventInfo.IsMulticast;
        }

        public EventMetadata(EventMetadataBase eventMetadata)
        {
            Name = eventMetadata.Name;
            TypeMetadata = ModelDTG.TypeMetadata.EmitSurrogateTypeMetadata(eventMetadata.TypeMetadata);
            AddMethodMetadata = eventMetadata.AddMethodMetadata == null
                ? null
                : new MethodMetadata(eventMetadata.AddMethodMetadata);
            RaiseMethodMetadata = eventMetadata.RaiseMethodMetadata == null
                ? null
                : new MethodMetadata(eventMetadata.RaiseMethodMetadata);
            RemoveMethodMetadata = eventMetadata.RemoveMethodMetadata == null
                ? null
                : new MethodMetadata(eventMetadata.RemoveMethodMetadata);
            Multicast = eventMetadata.Multicast;
            EventAttributes = GetTypesMetadata(eventMetadata.EventAttributes);
        }

        #endregion

        #region Properties

        public string Name { get; set; }

        public TypeMetadata TypeMetadata { get; set; }

        public MethodMetadata AddMethodMetadata { get; set; }

        public MethodMetadata RaiseMethodMetadata { get; set; }

        public MethodMetadata RemoveMethodMetadata { get; set; }

        public bool Multicast { get; set; }

        public IEnumerable<TypeMetadata> EventAttributes { get; set; }

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
                EventAttributes = GetOriginalTypesMetadata(EventAttributes)
            };
        }
    }
}