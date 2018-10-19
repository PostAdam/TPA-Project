using System.Collections.Generic;
using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class AssemblyMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        public AssemblyMetadataViewModel( AssemblyMetadata assemblyMetadata )
        {
            Name = assemblyMetadata.Name;
            _namespaces = assemblyMetadata.Namespaces;
        }

        #endregion

        private readonly IEnumerable<NamespaceMetadata> _namespaces;

        protected override void BuildMyself()
        {
            Child.Clear();
            foreach (NamespaceMetadata _namespace in _namespaces)
            {
                Child.Add( new NamespaceMetadataViewModel( _namespace ) );
                WasBuilt = true;
            }
        }
    }
}