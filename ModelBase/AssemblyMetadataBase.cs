using System.Collections.Generic;

namespace ModelBase
{
    public class AssemblyMetadataBase
    {
        #region Properties

        public string Name { get; set; }
        public IEnumerable<NamespaceMetadataBase> Namespaces { get; set; }

        #endregion
    }
}