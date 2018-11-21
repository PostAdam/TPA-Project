using System.Collections.Generic;
using Model.Reflection.MetadataModelBases;
using Model.Reflection.NewMetadataModels;

namespace ViewModel.MetadataViewModels
{
    public class AssemblyMetadataViewModel : MetadataBaseViewModel
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

        public string Name { get; internal set; }

        #endregion

        #region Private

        private readonly IEnumerable<NamespaceMetadataBase> _namespaces;

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();
            foreach (NamespaceMetadata _namespace in _namespaces)
            {
                Children.Add(new NamespaceMetadataViewModel(_namespace));
                WasBuilt = true;
            }
        }
    }
}