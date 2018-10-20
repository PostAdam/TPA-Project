using Project.Model.Reflection.Model;
using System.Collections.Generic;

namespace Project.ViewModel
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