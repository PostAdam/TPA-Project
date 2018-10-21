using Project.Model.Reflection.Model;
using ReflectionMVM.ViewModel;

namespace Project.ViewModel
{
    public class PropertyMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            return Modifier + " " + Name + ": " + TypeName;
        }

        #endregion

        #region Constructor

        internal PropertyMetadataViewModel(PropertyMetadata propertyMetadata)
        {
            _propertyMetadata = propertyMetadata;
            Modifier = GetModifierName(propertyMetadata.Modifiers?.Item1);
            TypeName = _propertyMetadata.TypeMetadata.TypeName;
            Name = propertyMetadata.Name;
        }

        #endregion

        #region Private

        private readonly PropertyMetadata _propertyMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();

            if (TypesDictionary.ReflectedTypes.ContainsKey(_propertyMetadata.TypeMetadata.TypeName))
            {
                Child.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_propertyMetadata.TypeMetadata.TypeName]));
            }
            else
            {
                Child.Add(new TypeMetadataViewModel(_propertyMetadata.TypeMetadata));
            }

            foreach (var attribute in _propertyMetadata.PropertyAttributes)
            {
                if (TypesDictionary.ReflectedTypes.ContainsKey(attribute.TypeName))
                {
                    Child.Add(new TypeMetadataViewModel(
                        TypesDictionary.ReflectedTypes[attribute.TypeName]));
                }
                else
                {
                    Child.Add(new TypeMetadataViewModel(attribute));
                }
            }


            WasBuilt = true;
        }
    }
}