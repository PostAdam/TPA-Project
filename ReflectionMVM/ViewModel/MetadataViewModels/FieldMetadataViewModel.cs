using System.Diagnostics.Eventing.Reader;
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
            string isStatic = _fieldMetadata.IsStatic == StaticEnum.Static ? "static " : "";
            return modifier + isStatic + Name + ": " + TypeName;
        }

        #endregion

        #region Constructor

        internal FieldMetadataViewModel(FieldMetadata fieldMetadata)
        {
            _fieldMetadata = fieldMetadata;
            Name = _fieldMetadata.Name;
            TypeName = _fieldMetadata.TypeMetadata.TypeName;
            Modifier = _fieldMetadata.Modifiers.ToString().ToLower();
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
                if (TypesDictionary.ReflectedTypes.ContainsKey(attribute.TypeName))
                {
                    Child.Add(new AttributeMetadataViewModel(
                        TypesDictionary.ReflectedTypes[attribute.TypeName]));
                }
                else
                {
                    Child.Add(new AttributeMetadataViewModel(attribute));
                }
            }

            WasBuilt = true;
        }
    }
}