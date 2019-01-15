using System.Collections.Generic;

namespace ModelBase
{
    public class NamespaceMetadataBase
    {
        #region Properties

        public string NamespaceName { get; set; }
        public IEnumerable<TypeMetadataBase> Types { get; set; }

        #endregion
    }
}