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
            FieldAttributes = TypeMetadata.EmitAttributes(fieldInfo.GetCustomAttributes());
            Modifiers = GetModifier(fieldInfo);
            IsStatic = fieldInfo.IsStatic ? StaticEnum.Static : StaticEnum.NotStatic;
        }

        #endregion

        #region Properties

        [DataMember] public string Name { get; set; }
        [DataMember] public TypeMetadata TypeMetadata { get; set; }
        [DataMember] public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        [DataMember] public StaticEnum IsStatic { get; set; }
        [DataMember] public IEnumerable<TypeMetadata> FieldAttributes { get; set; }

        #endregion

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
    }
}