using System.Collections.Generic;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class ParameterMetadataBase
    {
        public abstract string Name { get; set; }
        public abstract TypeMetadata TypeMetadata { get; set; }
        public abstract int Position { get; set; }
        public abstract ParameterKindEnum Kind { get; set; }
        public abstract IEnumerable<TypeMetadataBase> ParameterAttributes { get; set; }
        public abstract string DefaultValue { get; set; }
    }
}