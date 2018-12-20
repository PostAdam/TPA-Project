using System.Collections.Generic;

namespace ModelBase
{
    public class EventMetadataBase
    {
        #region Properties

        public string Name { get; set; }
        public TypeMetadataBase TypeMetadata { get; set; }
        public MethodMetadataBase AddMethodMetadata { get; set; }
        public MethodMetadataBase RaiseMethodMetadata { get; set; }
        public MethodMetadataBase RemoveMethodMetadata { get; set; }
        public bool Multicast { get; set; }
        public IEnumerable<TypeMetadataBase> EventAttributes { get; set; }

        #endregion
    }
}