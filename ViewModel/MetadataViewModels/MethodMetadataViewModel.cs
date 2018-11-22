using System;
using System.Linq;
using System.Text;
using Model.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;

namespace ViewModel.MetadataViewModels
{
    public class MethodMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        // Methods
        public override string ToString()
        {
            string fullname = "";
            //TODO: add which class is being extended
            fullname += _methodMetadata.Extension ? "extension: " : String.Empty;
            fullname += StringUtility.GetAttributes( _methodMetadata.MethodAttributes );
            fullname += StringUtility.GetMethodModifiers( _methodMetadata.Modifiers );
            fullname += _methodMetadata.ReturnType != null ? _methodMetadata.ReturnType.TypeName + " " : String.Empty;
            fullname += StringUtility.GetGenerics( _methodMetadata.ReturnType );
            fullname += _methodMetadata.Name;
            fullname += !string.IsNullOrEmpty( GetParameterNames() ) ? "(" + GetParameterNames() + ")" : "()";

            return fullname;
        }

        #endregion

        #region Constructor

        internal MethodMetadataViewModel( MethodMetadata methodMetadata )
        {
            _methodMetadata = methodMetadata;
            if ( !_methodMetadata.Parameters.Any() && !_methodMetadata.MethodAttributes.Any() &&
                _methodMetadata.ReturnType == null )
            {
                Children.Clear();
            }
        }

        #endregion

        #region Private

        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;

        private readonly MethodMetadata _methodMetadata;

        private string GetParameterNames()
        {
            if ( !_methodMetadata.Parameters.Any() )
            {
                return "";
            }

            StringBuilder names = new StringBuilder();
            for ( int i = 0; i < _methodMetadata.Parameters.Count(); i++ )
            {
                ParameterMetadata parameter = _methodMetadata.Parameters.ElementAt( i );
                string fullname = "";

                fullname += StringUtility.GetAttributes( parameter.ParameterAttributes );
                fullname += parameter.Kind != ParameterKindEnum.None
                    ? parameter.Kind.ToString().ToLower() + " "
                    : String.Empty;
                fullname += parameter.TypeMetadata.TypeName + " ";
                fullname += parameter.Name;
                fullname += !string.IsNullOrEmpty( parameter?.DefaultValue )
                    ? " = " + parameter.DefaultValue
                    : String.Empty;
                names.Append( " " + fullname );
                if ( i < _methodMetadata.Parameters.Count() - 1 )
                {
                    names.Append( ", " );
                }
                else
                {
                    names.Append( " " );
                }
            }

            return names.ToString();
        }

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();

            foreach ( ParameterMetadata parameter in _methodMetadata.Parameters )
            {
                Children.Add( new ParameterMetadataViewModel( parameter ) );
            }

            if ( _methodMetadata.ReturnType != null )
                if ( _reflectedTypes.ContainsKey( _methodMetadata.ReturnType.FullName ) )
                {
                    Children.Add( new TypeMetadataViewModel(
                        _reflectedTypes[ _methodMetadata.ReturnType.FullName ] ) );
                }
                else
                {
                    Children.Add( new TypeMetadataViewModel( _methodMetadata.ReturnType ) );
                }

            foreach ( TypeMetadata attribute in _methodMetadata.MethodAttributes )
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