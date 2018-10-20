using Project.Model.Reflection.Model;
using ReflectionMVM.ViewModel;

namespace Project.ViewModel
{
    public class AttributeMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            return TypeName + " " + "[" + Name + "]";
        }

        #endregion

        #region Constructor

        internal AttributeMetadataViewModel(AttributeMetadata attributeMetadata)
        {
            _attributeMetadata = attributeMetadata;
            Name = _attributeMetadata.Name;
            TypeName = _attributeMetadata.TypeMetadata.TypeName;
        }

        #endregion

        #region Private

        private readonly AttributeMetadata _attributeMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey(_attributeMetadata.TypeMetadata.TypeName))
            {
                Child.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_attributeMetadata.TypeMetadata.TypeName]));
            }
            else
            {
                Child.Add(new TypeMetadataViewModel(_attributeMetadata.TypeMetadata));
            }

            WasBuilt = true;
        }
    }
}