using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Project.Model.Reflection.Model
{
    [DataContract( IsReference = true )]
    internal class MethodMetadata
    {
        internal static IEnumerable<MethodMetadata> EmitMethods( IEnumerable<MethodBase> methods )
        {
            return from MethodBase currentMethod in methods
                where currentMethod.GetVisible()
                select new MethodMetadata( currentMethod );
        }

        #region Private

        #region Variables

        [DataMember] internal string Name;
        [DataMember] internal bool Extension;
        [DataMember] internal TypeMetadata ReturnType;
        [DataMember] internal IEnumerable<ParameterMetadata> Parameters;
        [DataMember] internal IEnumerable<TypeMetadata> GenericArguments;
        [DataMember] internal Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers;

        #endregion

        #region Private Constructor

        private MethodMetadata( MethodBase method )
        {
            Name = method.Name;
            GenericArguments = method.IsGenericMethodDefinition
                ? TypeMetadata.EmitGenericArguments( method.GetGenericArguments() )
                : null;
            ReturnType = EmitReturnType( method );
            Parameters = EmitParameters( method.GetParameters() );
            Modifiers = EmitModifiers( method );
            Extension = EmitExtension( method );
        }

        #endregion

        #region Emiters

        private static IEnumerable<ParameterMetadata> EmitParameters( IEnumerable<ParameterInfo> parms )
        {
            foreach (ParameterInfo parameter in parms)
            {
                if (TypesDictionary.ReflectedTypes.ContainsKey( parameter.ParameterType.Name ) == false)
                {
                    new TypeMetadata( parameter.ParameterType );
                }
            }

            return from parm in parms
                select new ParameterMetadata( parm.Name, TypeMetadata.EmitReference( parm.ParameterType ) );
        }

        private static TypeMetadata EmitReturnType( MethodBase method )
        {
            MethodInfo methodInfo = method as MethodInfo;
            if (methodInfo == null)
                return null;
            return TypeMetadata.EmitReference( methodInfo.ReturnType );
        }

        private static bool EmitExtension( MethodBase method )
        {
            return method.IsDefined( typeof(ExtensionAttribute), true );
        }

        private static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers( MethodBase method )
        {
            AccessLevel access = AccessLevel.IsPrivate;
            if (method.IsPublic)
                access = AccessLevel.IsPublic;
            else if (method.IsFamily)
                access = AccessLevel.IsProtected;
            else if (method.IsFamilyAndAssembly)
                access = AccessLevel.IsProtectedInternal;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if (method.IsAbstract)
                _abstract = AbstractEnum.Abstract;

            StaticEnum _static = StaticEnum.NotStatic;
            if (method.IsStatic)
                _static = StaticEnum.Static;

            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if (method.IsVirtual)
                _virtual = VirtualEnum.Virtual;

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( access, _abstract, _static,
                _virtual );
        }

        #endregion

        #endregion
    }
}