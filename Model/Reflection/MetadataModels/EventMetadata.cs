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
            TypeMetadata = TypeMetadata.EmitType(eventInfo.EventHandlerType);
            Name = eventInfo.Name;
            EventAttributes = TypeMetadata.EmitAttributes(eventInfo.GetCustomAttributes());
            AddMethodMetadata = MethodMetadata.EmitMethod(eventInfo.AddMethod);
            RaiseMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RaiseMethod);
            RemoveMethodMetadata = MethodMetadata.EmitMethod(eventInfo.RemoveMethod);
            Multicast = eventInfo.IsMulticast;
        }

        #endregion

        #region Internals

        [DataMember] public string Name;
        [DataMember] public TypeMetadata TypeMetadata;
        [DataMember] public MethodMetadata AddMethodMetadata;
        [DataMember] public MethodMetadata RaiseMethodMetadata;
        [DataMember] public MethodMetadata RemoveMethodMetadata;
        [DataMember] public bool Multicast;
        [DataMember] public IEnumerable<TypeMetadata> EventAttributes;

        #endregion
    }
}