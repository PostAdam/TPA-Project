using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class ParameterMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal ParameterMetadataViewModel( ParameterMetadata parameterMetadata )
        {
            Name = parameterMetadata.Name;
            _parameterMetadata = parameterMetadata;
        }

        #endregion

        private readonly ParameterMetadata _parameterMetadata;

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey( _parameterMetadata.TypeMetadata.TypeName ))
            {
                Child.Add( new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_parameterMetadata.TypeMetadata.TypeName] ) );
            }
            else
            {
                Child.Add( new TypeMetadataViewModel( _parameterMetadata.TypeMetadata ) );
            }

            WasBuilt = true;
        }
    }
}