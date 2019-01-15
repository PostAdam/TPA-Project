using System;
using System.Collections.Generic;
using ModelBase.Enums;

namespace ModelBase
{
    public class MethodMetadataBase
    {
        #region Properties

        public string Name { get; set; }
        public bool Extension { get; set; }
        public TypeMetadataBase ReturnType { get; set; }
        public IEnumerable<TypeMetadataBase> MethodAttributes { get; set; }
        public IEnumerable<ParameterMetadataBase> Parameters { get; set; }
        public IEnumerable<TypeMetadataBase> GenericArguments { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }

        #endregion
    }
}