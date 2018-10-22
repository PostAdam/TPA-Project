using Model.Reflection;
using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class AttributeMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetTypeModifiers(_attributeMetadata.Modifiers);
            fullname += _attributeMetadata.TypeName;
            return fullname;
        }

        #endregion

        #region Constructor

        internal AttributeMetadataViewModel(TypeMetadata attributeMetadata)
        {
            _attributeMetadata = attributeMetadata;
        }

        #endregion

        #region Private

        private readonly TypeMetadata _attributeMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey(_attributeMetadata.FullName))
            {
                Children.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_attributeMetadata.FullName]));
            }
            else
            {
                Children.Add(new TypeMetadataViewModel(_attributeMetadata));
            }

            WasBuilt = true;
        }
    }
}