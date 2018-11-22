﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModelBases;

namespace Model.Reflection.NewMetadataModels
{
    public class MethodMetadata : MethodMetadataBase
    {
        public MethodMetadata()
        {
        }

        #region Emit API

        internal static IEnumerable<MethodMetadata> EmitMethods( IEnumerable<MethodBase> methods )
        {
            return from MethodBase method in methods select new MethodMetadata( method );
        }

        internal static MethodMetadata EmitMethod( MethodBase method )
        {
            if ( method == null )
                return null;

            return new MethodMetadata( method );
        }

        #endregion

        #region Properties

        public override string Name { get; set; }
        public override bool Extension { get; set; }
        public override TypeMetadataBase ReturnType { get; set; }
        public override IEnumerable<TypeMetadataBase> MethodAttributes { get; set; }
        public override IEnumerable<ParameterMetadataBase> Parameters { get; set; }
        public override IEnumerable<TypeMetadataBase> GenericArguments { get; set; }
        public override Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        #endregion

        #region Private Constructor

        private MethodMetadata( MethodBase method )
        {
            Name = method.Name;
            GenericArguments = method.IsGenericMethodDefinition
                ? TypeMetadata.EmitTypes( method.GetGenericArguments() )
                : null;
            ReturnType = EmitReturnType( method );
            Parameters = EmitParameters( method.GetParameters() );
            Modifiers = EmitModifiers( method );
            MethodAttributes = TypeMetadata.EmitAttributes( method.GetCustomAttributes() );
            Extension = EmitExtension( method );
        }

        #endregion

        #region Emiters

        private static IEnumerable<ParameterMetadata> EmitParameters( IList<ParameterInfo> parameters )
        {
            return from parameter in parameters select new ParameterMetadata( parameter );
        }

        private static TypeMetadataBase EmitReturnType( MethodBase method )
        {
            MethodInfo methodInfo = method as MethodInfo;
            return TypeMetadata.EmitType( methodInfo?.ReturnType );
        }

        private static bool EmitExtension( MethodBase method )
        {
            return method.IsDefined( typeof( ExtensionAttribute ), true );
        }

        public static Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> EmitModifiers( MethodBase method )
        {
            AccessLevel access = AccessLevel.Private;
            if ( method.IsPublic )
                access = AccessLevel.Public;
            else if ( method.IsFamily )
                access = AccessLevel.Protected;
            else if ( method.IsAssembly )
                access = AccessLevel.Internal;

            AbstractEnum _abstract = AbstractEnum.NotAbstract;
            if ( method.IsAbstract )
                _abstract = AbstractEnum.Abstract;

            StaticEnum _static = StaticEnum.NotStatic;
            if ( method.IsStatic )
                _static = StaticEnum.Static;

            VirtualEnum _virtual = VirtualEnum.NotVirtual;
            if ( method.IsVirtual )
                _virtual = VirtualEnum.Virtual;

            return new Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum>( access, _abstract, _static,
                _virtual );
        }

        #endregion
    }
}