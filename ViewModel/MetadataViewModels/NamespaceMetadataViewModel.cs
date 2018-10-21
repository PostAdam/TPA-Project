using System.Collections.Generic;
using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class NamespaceMetadataViewModel : MetadataViewModel
    {
        #region Public

        public override string ToString()
        {
            return Name;
        }

        #endregion

        #region Constructor

        public NamespaceMetadataViewModel(NamespaceMetadata namespaceMetadata)
        {
            Types = namespaceMetadata.Types;
            Name = namespaceMetadata.NamespaceName;
        }

        #endregion

        #region Private

        private readonly IEnumerable<TypeMetadata> Types;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();
            foreach (TypeMetadata type in Types)
                Child.Add(new TypeMetadataViewModel(type));
            WasBuilt = true;
        }
    }
}