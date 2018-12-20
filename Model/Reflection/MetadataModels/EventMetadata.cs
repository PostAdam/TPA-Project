using System.Collections.Generic;
using System.Reflection;
using ModelBase;

namespace Model.Reflection.MetadataModels
{
    public class EventMetadata
    {
        #region Constructors

        public EventMetadata()
        {
        }

        internal EventMetadata(EventInfo eventInfo)
        {
            Name = eventInfo.Name;
            TypeMetadata = MetadataModels.TypeMetadata.EmitType(eventInfo.EventHandlerType);
            EventAttributes = MetadataModels.TypeMetadata.EmitAttributes(eventInfo.GetCustomAttributes());
            AddMethodMetadata = MethodMetadata.EmitMethod(eventInfo.AddMethod);
            RaiseMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RaiseMethod);
            RemoveMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RemoveMethod);
            Multicast = eventInfo.IsMulticast;
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
        public EventMetadataBase EventMetadataBase { get; set; }

        #endregion
    }
}