using System.Collections.Generic;
using System.Reflection;
using ModelBase;

namespace Model.Reflection.MetadataModels
{
    public class EventMetadata : EventMetadataBase
    {
        #region Constructors

        public EventMetadata()
        { }

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
    }
}