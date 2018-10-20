using System;
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
            TypeMetadata = TypeMetadata.EmitReference(eventInfo.EventHandlerType);
            Name = eventInfo.Name;
            Modifiers = TypeMetadata.Modifiers;
            EventAttributes = TypeMetadata.EmitAttributes(eventInfo.GetCustomAttributes());
            AddMethodMetadata = 
                eventInfo.AddMethod != null ? MethodMetadata.EmitMethod(eventInfo.AddMethod) : null;
            RaiseMethodMetadata =
                eventInfo.RaiseMethod != null ? MethodMetadata.EmitMethod(eventInfo.RaiseMethod) : null;
            RemoveMethodMetadata =
                eventInfo.RemoveMethod != null ? MethodMetadata.EmitMethod(eventInfo.RemoveMethod) : null;
            Multicast = eventInfo.IsMulticast;
        }

        #endregion

        #region Internals

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;
        [DataMember] internal Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;
        [DataMember] internal MethodMetadata AddMethodMetadata;
        [DataMember] internal MethodMetadata RaiseMethodMetadata;
        [DataMember] internal MethodMetadata RemoveMethodMetadata;
        [DataMember] internal bool Multicast;
        [DataMember] internal IEnumerable<AttributeMetadata> EventAttributes;

        #endregion
    }
}