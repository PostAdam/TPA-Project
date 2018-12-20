using System.Collections.Generic;
using Model.Reflection.Enums;

namespace ModelBase
{
    public class ParameterMetadataBase
    {
        #region Properties

        public string Name { get; set; }
        public TypeMetadataBase TypeMetadata { get; set; }
        public int Position { get; set; }
        public ParameterKindEnum Kind { get; set; }
        public IEnumerable<TypeMetadataBase> ParameterAttributes { get; set; }
        public string DefaultValue { get; set; }

        #endregion
    }
}