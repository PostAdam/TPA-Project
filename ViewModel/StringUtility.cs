using System;
using System.Collections.Generic;
using System.Linq;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace ViewModel
{
    public class StringUtility
    {
        public static string GetAttributes(IEnumerable<TypeMetadata> attributesMetadata)
        {
            if (attributesMetadata == null) return null;
            string attributes = "";

            foreach (var attributeMetadata in attributesMetadata)
            {
                attributes += "[" + attributeMetadata.TypeName.Replace("Attribute", String.Empty) + "]";
            }

            if (!string.IsNullOrEmpty(attributes)) attributes += " ";

            return attributes;
        }

        public static string GetTypeModifiers(Tuple<AccessLevel, SealedEnum, AbstractEnum> modifiers)
        {
            if (modifiers == null) return String.Empty;

            string mods = String.Empty;
            mods += modifiers.Item1 + " ";
            mods += modifiers.Item2 == SealedEnum.Sealed ? modifiers.Item2 + " " : String.Empty;
            mods += modifiers.Item3 == AbstractEnum.Abstract ? modifiers.Item3 + " " : String.Empty;
            return mods.ToLower();
        }

        public static string GetMethodModifiers(Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> modifiers)
        {
            if (modifiers == null) return String.Empty;

            string mods = String.Empty;
            mods += modifiers.Item1 + " ";
            mods += modifiers.Item2 == AbstractEnum.Abstract ? modifiers.Item2 + " " : String.Empty;
            mods += modifiers.Item3 == StaticEnum.Static ? modifiers.Item3 + " " : String.Empty;
            mods += modifiers.Item4 == VirtualEnum.Virtual ? modifiers.Item4 + " " : String.Empty;
            return mods.ToLower();
        }

        public static string GetGenerics(TypeMetadata typeMetadata)
        {
            string generics = String.Empty;
            if (typeMetadata?.GenericArguments != null && typeMetadata.GenericArguments.Any())
            {
                generics += "<";
                for (int i = 0; i < typeMetadata.GenericArguments.Count(); i++)
                {
                    generics += typeMetadata.GenericArguments.ElementAt(i).TypeName;
                    if (i < typeMetadata.GenericArguments.Count() - 1)
                    {
                        generics += ", ";
                    }
                    else
                    {
                        generics += ">";
                    }
                }
            }

            return generics;
        }
    }
}