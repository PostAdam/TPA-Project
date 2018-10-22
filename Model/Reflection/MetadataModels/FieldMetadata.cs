using System;
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
        [DataMember] public Tuple<AccessLevel, StaticEnum> Modifiers;
        [DataMember] public StaticEnum IsStatic;
        [DataMember] public IEnumerable<TypeMetadata> FieldAttributes;

        internal static Tuple<AccessLevel, StaticEnum> GetModifier(FieldInfo fieldInfo)
        {
            AccessLevel access = AccessLevel.Private;
            if (fieldInfo.IsPublic)
                access = AccessLevel.Public;
            else if (fieldInfo.IsFamilyOrAssembly)
                access = AccessLevel.Public;
            else if (fieldInfo.IsFamily)
                access = AccessLevel.Protected;
            else if (fieldInfo.IsAssembly)
                access = AccessLevel.Internal;

            StaticEnum _static = StaticEnum.NotStatic;
            if (fieldInfo.IsStatic)
                _static = StaticEnum.Static;

            return new Tuple<AccessLevel, StaticEnum>(access,_static);
        }

        #endregion
    }
}