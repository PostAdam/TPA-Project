using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using Model.Reflection.Enums;

namespace Model.Reflection.MetadataModels
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

        [DataMember] public string Name;
        [DataMember] public TypeMetadata TypeMetadata;
        [DataMember] public AccessLevel Modifiers;
        [DataMember] public StaticEnum IsStatic;
        [DataMember] public IEnumerable<TypeMetadata> FieldAttributes;

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