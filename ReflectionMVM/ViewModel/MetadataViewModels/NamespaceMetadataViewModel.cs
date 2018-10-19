using Project.Model.Reflection.Model;
using System.Collections.Generic;

namespace Project.ViewModel
{
    public class NamespaceMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        public NamespaceMetadataViewModel( NamespaceMetadata namespaceMetadata )
        {
            Types = namespaceMetadata.Types;
            Name = namespaceMetadata.NamespaceName;
        }

        #endregion

        internal readonly IEnumerable<TypeMetadata> Types;

        protected override void BuildMyself()
        {
            Child.Clear();
            foreach (TypeMetadata type in Types)
                Child.Add( new TypeMetadataViewModel( type ) );
            WasBuilt = true;
        }
    }
}