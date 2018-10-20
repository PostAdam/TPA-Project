using Project.Model.Reflection.Model;
using ReflectionMVM.ViewModel;

namespace Project.ViewModel
{
    public class ParameterMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Public

        public string TypeName { get; }

        public override string ToString()
        {
            return Name + ": " + TypeName;
        }

        #endregion

        #region Constructor

        internal ParameterMetadataViewModel(ParameterMetadata parameterMetadata)
        {
            _parameterMetadata = parameterMetadata;
            TypeName = _parameterMetadata.TypeMetadata.TypeName;
            Name = _parameterMetadata.Name;
        }

        #endregion

        #region Private

        private readonly ParameterMetadata _parameterMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey(_parameterMetadata.TypeMetadata.TypeName))
            {
                Child.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_parameterMetadata.TypeMetadata.TypeName]));
            }
            else
            {
                Child.Add(new TypeMetadataViewModel(_parameterMetadata.TypeMetadata));
            }

            WasBuilt = true;
        }
    }
}