using System.Collections.Generic;
using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class AssemblyMetadataViewModel : MetadataViewModel
    {
        #region Public

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Constructor

        public AssemblyMetadataViewModel(AssemblyMetadata assemblyMetadata)
        {
            Name = assemblyMetadata.Name;
            _namespaces = assemblyMetadata.Namespaces;
        }

        #endregion

        #region Private

        private readonly IEnumerable<NamespaceMetadata> _namespaces;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();
            foreach (NamespaceMetadata _namespace in _namespaces)
            {
                Child.Add(new NamespaceMetadataViewModel(_namespace));
                WasBuilt = true;
            }
        }
    }
}