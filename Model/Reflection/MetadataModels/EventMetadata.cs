using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Model.Reflection.MetadataModels
{
    [DataContract(IsReference = true)]
    public class EventMetadata
    {
        #region Constructor

        internal EventMetadata(EventInfo eventInfo)
        {
            Name = eventInfo.Name;
            TypeMetadata = TypeMetadata.EmitType(eventInfo.EventHandlerType);
            EventAttributes = TypeMetadata.EmitAttributes(eventInfo.GetCustomAttributes());
            AddMethodMetadata = MethodMetadata.EmitMethod(eventInfo.AddMethod);
            RaiseMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RaiseMethod);
            RemoveMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RemoveMethod);
            Multicast = eventInfo.IsMulticast;
        }

        #endregion

        #region Properties

        [DataMember] public string Name { get; set; }
        [DataMember] public TypeMetadata TypeMetadata { get; set; }
        [DataMember] public MethodMetadata AddMethodMetadata { get; set; }
        [DataMember] public MethodMetadata RaiseMethodMetadata { get; set; }
        [DataMember] public MethodMetadata RemoveMethodMetadata { get; set; }
        [DataMember] public bool Multicast { get; set; }
        [DataMember] public IEnumerable<TypeMetadata> EventAttributes { get; set; }

        #endregion
    }
}