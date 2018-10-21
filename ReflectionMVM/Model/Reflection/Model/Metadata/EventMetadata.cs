using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
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

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;
        [DataMember] internal MethodMetadata AddMethodMetadata;
        [DataMember] internal MethodMetadata RaiseMethodMetadata;
        [DataMember] internal MethodMetadata RemoveMethodMetadata;
        [DataMember] internal bool Multicast;
        [DataMember] internal IEnumerable<TypeMetadata> EventAttributes;

        #endregion
    }
}