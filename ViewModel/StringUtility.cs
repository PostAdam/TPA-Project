using System;
using System.Collections.Generic;
using System.Linq;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using static System.String;

namespace ViewModel
{
    public class StringUtility
    {
        public static string GetAttributes(IEnumerable<TypeMetadata> attributesMetadata)
        {
            if (attributesMetadata == null) return Empty;

            string attributes = Empty;

            foreach (TypeMetadata attributeMetadata in attributesMetadata)
            {
                attributes += "[" + attributeMetadata.TypeName.Replace("Attribute", Empty) + "]";
            }

            if (!IsNullOrEmpty(attributes)) attributes += " ";

            return attributes;
        }

        public static string GetTypeModifiers(Tuple<AccessLevel, SealedEnum, AbstractEnum> modifiers)
        {
            if (modifiers == null) return Empty;

            string mods = Empty;
            mods += modifiers.Item1 + " ";
            mods += modifiers.Item2 == SealedEnum.Sealed ? modifiers.Item2 + " " : Empty;
            mods += modifiers.Item3 == AbstractEnum.Abstract ? modifiers.Item3 + " " : Empty;
            return mods.ToLower();
        }

        public static string GetMethodModifiers(Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> modifiers)
        {
            if (modifiers == null) return Empty;

            string mods = Empty;
            mods += modifiers.Item1 + " ";
            mods += modifiers.Item2 == AbstractEnum.Abstract ? modifiers.Item2 + " " : Empty;
            mods += modifiers.Item3 == StaticEnum.Static ? modifiers.Item3 + " " : Empty;
            mods += modifiers.Item4 == VirtualEnum.Virtual ? modifiers.Item4 + " " : Empty;
            return mods.ToLower();
        }

        public static string GetGenerics(TypeMetadata typeMetadata)
        {
            if (typeMetadata?.GenericArguments == null) return Empty;

            string generics = Empty;
            if (typeMetadata.GenericArguments.Any())
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