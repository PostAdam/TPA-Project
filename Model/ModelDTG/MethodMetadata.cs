using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Model.Reflection.MetadataModels;
using ModelBase;
using Model.Reflection.Enums;
using static Model.ModelDTG.Accessors.CollectionOriginalTypeAccessor;
using static Model.ModelDTG.Accessors.CollectionTypeAccessor;

namespace Model.ModelDTG
{
    public class MethodMetadata
    {
        public MethodMetadata()
        {
        }

        #region Emit API

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

        public MethodMetadata(MethodMetadataBase methodMetadata)
        {
            Name = methodMetadata.Name;
            Extension = methodMetadata.Extension;
            ReturnType = TypeMetadata.EmitSurrogateTypeMetadata(methodMetadata.ReturnType);
            MethodAttributes = GetTypesMetadata(methodMetadata.MethodAttributes);
            Parameters = GetParametersMetadata(methodMetadata.Parameters);
            GenericArguments = GetTypesMetadata(methodMetadata.GenericArguments);
            Modifiers = Tuple.Create((AccessLevel) methodMetadata.Modifiers.Item1,
                (AbstractEnum) methodMetadata.Modifiers.Item2,
                (StaticEnum) methodMetadata.Modifiers.Item3,
                (VirtualEnum) methodMetadata.Modifiers.Item4);
        }

        #endregion

        #region Private Constructor

        private MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            GenericArguments = method.IsGenericMethodDefinition
                ? TypeReflector.EmitTypes(method.GetGenericArguments())
                : null;
            ReturnType = MethodReflector.EmitReturnType(method);
            Parameters = MethodReflector.EmitParameters(method.GetParameters());
            Modifiers = MethodReflector.EmitModifiers(method);
            MethodAttributes = TypeReflector.EmitAttributes(method.CustomAttributes);
            Extension = MethodReflector.EmitExtension(method);
        }

        #endregion

        #region Properties

        public string Name { get; set; }
        public bool Extension { get; set; }
        public TypeMetadata ReturnType { get; set; }
        public IEnumerable<TypeMetadata> MethodAttributes { get; set; }
        public IEnumerable<ParameterMetadata> Parameters { get; set; }
        public IEnumerable<TypeMetadata> GenericArguments { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        #endregion

        public MethodMetadataBase GetOriginalMethodMetadata()
        {
            return new MethodMetadataBase()
            {
                Name = Name,
                Extension = Extension,
                ReturnType = ReturnType?.EmitOriginalTypeMetadata(),
                MethodAttributes = GetOriginalTypesMetadata(MethodAttributes),
                Parameters = GetOriginalParametersMetadata(Parameters),
                GenericArguments = GetOriginalTypesMetadata(GenericArguments),
                Modifiers = Tuple.Create((ModelBase.Enums.AccessLevel) Modifiers.Item1,
                    (ModelBase.Enums.AbstractEnum) Modifiers.Item2,
                    (ModelBase.Enums.StaticEnum) Modifiers.Item3, (ModelBase.Enums.VirtualEnum) Modifiers.Item4)
            };
        }
    }
}