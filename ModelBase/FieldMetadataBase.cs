using System;
using System.Collections.Generic;
using Model.Reflection.Enums;

namespace ModelBase
{
    public class FieldMetadataBase
    {
        #region Properties

        public string Name { get; set; }
        public TypeMetadataBase TypeMetadata { get; set; }
        public Tuple<AccessLevel, StaticEnum> Modifiers { get; set; }
        public StaticEnum IsStatic { get; set; }
        public IEnumerable<TypeMetadataBase> FieldAttributes { get; set; }

        #endregion
    }
}