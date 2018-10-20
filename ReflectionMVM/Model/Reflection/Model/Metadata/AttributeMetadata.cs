using System;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class AttributeMetadata
    {
        #region Constructor

        internal AttributeMetadata(string name, TypeMetadata typeMetadata)
        {
            Name = name;
            TypeMetadata = typeMetadata;
            Modifiers = typeMetadata.Modifiers;
        }

        #endregion

        #region Internals

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;
        [DataMember] internal Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;

        #endregion
    }
}