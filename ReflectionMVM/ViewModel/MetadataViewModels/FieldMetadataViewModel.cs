using Project.Model.Reflection.Model;
using ReflectionMVM.ViewModel;

namespace Project.ViewModel
{
    public class FieldMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string modifier = string.IsNullOrEmpty(Modifier) ? Modifier : Modifier + " ";
            return modifier + TypeName + " " + Name;
        }

        #endregion

        #region Constructor

        internal FieldMetadataViewModel(FieldMetadata fieldMetadata)
        {
            _fieldMetadata = fieldMetadata;
            Modifier = GetModifierName(_fieldMetadata.TypeMetadata.Modifiers?.Item1);
            TypeName = _fieldMetadata.TypeMetadata.TypeName;
            Name = _fieldMetadata.Name;
        }

        #endregion

        #region Private 

        private readonly FieldMetadata _fieldMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey(_fieldMetadata.TypeMetadata.TypeName))
            {
                Child.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_fieldMetadata.TypeMetadata.TypeName]));
            }
            else
            {
                Child.Add(new TypeMetadataViewModel(_fieldMetadata.TypeMetadata));
            }

            foreach (var attribute in _fieldMetadata.FieldAttributes)
            {
                if (TypesDictionary.ReflectedTypes.ContainsKey(attribute.TypeMetadata.TypeName))
                {
                    Child.Add(new TypeMetadataViewModel(
                        TypesDictionary.ReflectedTypes[attribute.TypeMetadata.TypeName]));
                }
                else
                {
                    Child.Add(new TypeMetadataViewModel(attribute.TypeMetadata));
                }
            }

            WasBuilt = true;
        }
    }
}