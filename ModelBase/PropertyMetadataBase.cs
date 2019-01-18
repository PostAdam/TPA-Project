using System;
using System.Collections.Generic;
using ModelBase.Enums;

namespace ModelBase
{
    public class PropertyMetadataBase
    {
        #region Properties

        public string Name { get; set; }
        public IEnumerable<TypeMetadataBase> PropertyAttributes { get; set; }
        public Tuple<AccessLevel, AbstractEnum, StaticEnum, VirtualEnum> Modifiers { get; set; }
        public TypeMetadataBase TypeMetadata { get; set; }
        public MethodMetadataBase Getter { get; set; }
        public MethodMetadataBase Setter { get; set; }

        #endregion
    }
}