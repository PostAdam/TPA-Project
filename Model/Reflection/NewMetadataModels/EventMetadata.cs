using System.Collections.Generic;
using System.Reflection;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
{
    public class EventMetadata : EventMetadataBase
    {
        #region Constructors

        public EventMetadata()
        { }

        internal EventMetadata(EventInfo eventInfo)
        {
            Name = eventInfo.Name;
            TypeMetadata = TypeMetadataBase.EmitType(eventInfo.EventHandlerType);
            EventAttributes = TypeMetadataBase.EmitAttributes(eventInfo.GetCustomAttributes());
            AddMethodMetadata = MethodMetadata.EmitMethod(eventInfo.AddMethod);
            RaiseMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RaiseMethod);
            RemoveMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RemoveMethod);
            Multicast = eventInfo.IsMulticast;
        }

        #endregion

        #region Properties

        public override string Name { get; set; }
        public override TypeMetadataBase TypeMetadata { get; set; }
        public override MethodMetadataBase AddMethodMetadata { get; set; }
        public override MethodMetadataBase RaiseMethodMetadata { get; set; }
        public override MethodMetadataBase RemoveMethodMetadata { get; set; }
        public override bool Multicast { get; set; }
        public override IEnumerable<TypeMetadataBase> EventAttributes { get; set; }

        #endregion
    }
}