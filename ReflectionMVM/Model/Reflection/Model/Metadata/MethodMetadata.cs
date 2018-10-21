﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract(IsReference = true)]
    public class MethodMetadata
    {
        internal static IEnumerable<MethodMetadata> EmitMethods(IEnumerable<MethodBase> methods)
        {
            return from MethodBase method in methods select new MethodMetadata(method);
        }

        internal static MethodMetadata EmitMethod(MethodBase method)
        {
            if (method == null)
                return null;

            return new MethodMetadata(method);
        }

        #region Private

        #region Variables

        [DataMember] internal string Name;
        [DataMember] internal bool Extension;
        [DataMember] internal TypeMetadata ReturnType;
        [DataMember] internal IEnumerable<TypeMetadata> MethodAttributes;
        [DataMember] internal IEnumerable<ParameterMetadata> Parameters;
        [DataMember] internal IEnumerable<TypeMetadata> GenericArguments;
        [DataMember] internal Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers;

        #endregion

        #region Private Constructor

        private MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = method.IsGenericMethodDefinition
                ? TypeMetadata.EmitTypes(method.GetGenericArguments())
                : null;
            ReturnType = EmitReturnType(method);
            Parameters = EmitParameters(method.GetParameters());
            Modifiers = EmitModifiers(method);
            Extension = EmitExtension(method);
            MethodAttributes = TypeMetadata.EmitAttributes(method.GetCustomAttributes());
        }

        #endregion

        #region Emiters

        private static IEnumerable<ParameterMetadata> EmitParameters(IList<ParameterInfo> parameters)
        {
            return from parameter in parameters select new ParameterMetadata(parameter);
        }

        private static TypeMetadata EmitReturnType(MethodBase method)
        {
            MethodInfo methodInfo = method as MethodInfo;
            return TypeMetadata.EmitType(methodInfo?.ReturnType);
        }

        private static bool EmitExtension(MethodBase method)
        {
            return method.IsDefined(typeof(ExtensionAttribute), true);
        }

        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers(MethodBase method)
        {
            AccessLevel access = AccessLevel.Private;
            if (method.IsPublic)
                access = AccessLevel.Public;
            else if (method.IsFamily)
                access = AccessLevel.Protected;
            else if (method.IsFamilyAndAssembly)
                access = AccessLevel.Internal;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;

            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>(access, _abstract, _static,
                _virtual);
        }

        #endregion

        #endregion
    }
}