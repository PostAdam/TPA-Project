using Model.ModelDTG;
using Model.Reflection;

namespace ViewModel.MetadataViewModels
{
    public class AttributeMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetTypeModifiers( _attributeMetadata.Modifiers );
            fullname += _attributeMetadata.TypeName;
            return fullname;
        }

        #endregion

        #region Constructor

        internal AttributeMetadataViewModel( TypeMetadata attributeMetadata )
        {
            _attributeMetadata = attributeMetadata;
        }

        #endregion

        #region Private

        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;
        private readonly TypeMetadata _attributeMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();
            if ( _reflectedTypes.ContainsKey( _attributeMetadata.FullName ) )
            {
                Children.Add( new TypeMetadataViewModel(
                    _reflectedTypes[ _attributeMetadata.FullName ] ) );
            }
            else
            {
                Children.Add( new TypeMetadataViewModel( _attributeMetadata ) );
            }

            WasBuilt = true;
        }
    }
}