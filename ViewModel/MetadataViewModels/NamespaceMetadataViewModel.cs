using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class NamespaceMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            return _namespaceMetadata.NamespaceName;
        }

        #endregion

        #region Constructor

        public NamespaceMetadataViewModel(NamespaceMetadata namespaceMetadata)
        {
            _namespaceMetadata = namespaceMetadata;
        }

        #endregion

        #region Private

        private readonly NamespaceMetadata _namespaceMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();
            foreach (TypeMetadata type in _namespaceMetadata.Types)
                Children.Add(new TypeMetadataViewModel(type));
            WasBuilt = true;
        }
    }
}