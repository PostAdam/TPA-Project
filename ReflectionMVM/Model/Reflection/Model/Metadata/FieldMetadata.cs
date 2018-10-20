using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract( IsReference = true )]
    public class FieldMetadata
    {
        #region Constructor
            
        internal FieldMetadata( string name, TypeMetadata typeMetadata, IEnumerable<AttributeMetadata> attributes)
        {
            Name = name;
            Modifiers = typeMetadata.Modifiers;
            TypeMetadata = typeMetadata;
            FieldAttributes = attributes;
        }

        #endregion

        #region Internals

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;
        [DataMember] internal Tuple<AccessLevel, SealedEnum, AbstractEnum> Modifiers;
        [DataMember] internal IEnumerable<AttributeMetadata> FieldAttributes;

        #endregion
    }
}