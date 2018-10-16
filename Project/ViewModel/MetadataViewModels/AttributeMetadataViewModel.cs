using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class AttributeMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal AttributeMetadataViewModel( AttributeMetadata attributeMetadata )
        {
            Name = attributeMetadata.Name;
            _attributeMetadata = attributeMetadata;
        }

        #endregion

        private readonly AttributeMetadata _attributeMetadata;

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey( _attributeMetadata.TypeMetadata.TypeName ))
            {
                Child.Add( new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_attributeMetadata.TypeMetadata.TypeName] ) );
            }
            else
            {
                Child.Add( new TypeMetadataViewModel( _attributeMetadata.TypeMetadata ) );
            }

            WasBuilt = true;
        }
    }
}