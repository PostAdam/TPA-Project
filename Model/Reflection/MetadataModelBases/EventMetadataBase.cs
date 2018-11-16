using System.Collections.Generic;

namespace Model.Reflection.MetadataModelBases
{
    public abstract class EventMetadataBase
    {
        public abstract string Name { get; set; }
        public abstract TypeMetadataBase TypeMetadata { get; set; }
        public abstract MethodMetadataBase AddMethodMetadata { get; set; }
        public abstract MethodMetadataBase RaiseMethodMetadata { get; set; }
        public abstract MethodMetadataBase RemoveMethodMetadata { get; set; }
        public abstract bool Multicast { get; set; }
        public abstract IEnumerable<TypeMetadataBase> EventAttributes { get; set; }
    }
}