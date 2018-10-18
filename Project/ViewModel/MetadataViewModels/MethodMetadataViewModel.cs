using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class MethodMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal MethodMetadataViewModel(MethodMetadata methodMetadata)
        {
            Name = GetModifier(methodMetadata.Modifiers?.Item1) + " " + methodMetadata.Name;
            _methodMetadata = methodMetadata;
        }

        #endregion

        private readonly MethodMetadata _methodMetadata;

        protected override void BuildMyself()
        {
            Child.Clear();
            foreach (ParameterMetadata parameter in _methodMetadata.Parameters)
            {
                Child.Add(new ParameterMetadataViewModel(parameter));
            }

            if(_methodMetadata.ReturnType != null)
            if (TypesDictionary.ReflectedTypes.ContainsKey(_methodMetadata.ReturnType.TypeName))
            {
                Child.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_methodMetadata.ReturnType.TypeName]));
            }
            else
            {
                Child.Add(new TypeMetadataViewModel(_methodMetadata.ReturnType));
            }

            WasBuilt = true;
        }
    }
}