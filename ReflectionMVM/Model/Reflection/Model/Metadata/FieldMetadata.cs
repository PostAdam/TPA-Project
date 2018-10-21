using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class FieldMetadata
    {
        #region Constructor

        internal FieldMetadata(FieldInfo fieldInfo)
        {
            Name = fieldInfo.Name;
            TypeMetadata = TypeMetadata.EmitType(fieldInfo.FieldType);
            Modifiers = GetModifier(fieldInfo);
            IsStatic = fieldInfo.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;
            FieldAttributes = TypeMetadata.EmitAttributes(fieldInfo.GetCustomAttributes());
        }

        #endregion

        #region Internals

        [DataMember] internal string Name;
        [DataMember] internal TypeMetadata TypeMetadata;
        [DataMember] internal AccessLevel Modifiers;
        [DataMember] internal StaticEnum IsStatic;
        [DataMember] internal IEnumerable<TypeMetadata> FieldAttributes;

        internal static AccessLevel GetModifier(FieldInfo fieldInfo)
        {
            AccessLevel access = AccessLevel.Private;
            if (fieldInfo.IsPublic)
                access = AccessLevel.Public;
            else if (fieldInfo.IsFamilyOrAssembly)
                access = AccessLevel.Public;
            else if (fieldInfo.IsFamily)
                access = AccessLevel.Protected;
            else if (fieldInfo.IsFamilyOrAssembly)
                access = AccessLevel.Internal;
            return access;
        }

        #endregion
    }
}