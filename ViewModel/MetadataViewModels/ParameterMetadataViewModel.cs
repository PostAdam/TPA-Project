using System;
using Model.ModelDTG;
using Model.Reflection;
using Model.Reflection.Enums;

namespace ViewModel.MetadataViewModels
{
    public class ParameterMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetAttributes( _parameterMetadata.ParameterAttributes );
            fullname += _parameterMetadata.Kind != ParameterKindEnum.None
                ? _parameterMetadata.Kind.ToString().ToLower() + " "
                : String.Empty;
            fullname += _parameterMetadata.TypeMetadata.TypeName + " ";
            fullname += _parameterMetadata.Name;
            fullname += !string.IsNullOrEmpty( _parameterMetadata?.DefaultValue )
                ? " = " + _parameterMetadata.DefaultValue
                : String.Empty;
            return fullname;
        }

        #endregion

        #region Constructor

        internal ParameterMetadataViewModel( ParameterMetadata parameterMetadata )
        {
            _parameterMetadata = parameterMetadata;
        }

        #endregion

        #region Private

        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;

        private readonly ParameterMetadata _parameterMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();
            if ( _reflectedTypes.ContainsKey( _parameterMetadata.TypeMetadata.FullName ) )
            {
                Children.Add( new TypeMetadataViewModel(
                    _reflectedTypes[ _parameterMetadata.TypeMetadata.FullName ] ) );
            }
            else
            {
                Children.Add( new TypeMetadataViewModel( _parameterMetadata.TypeMetadata ) );
            }

            foreach ( TypeMetadata attribute in _parameterMetadata.ParameterAttributes )
            {
                if ( _reflectedTypes.ContainsKey( attribute.FullName ) )
                {
                    Children.Add( new AttributeMetadataViewModel(
                        _reflectedTypes[ attribute.FullName ] ) );
                }
                else
                {
                    Children.Add( new AttributeMetadataViewModel( attribute ) );
                }
            }


            WasBuilt = true;
        }
    }
}