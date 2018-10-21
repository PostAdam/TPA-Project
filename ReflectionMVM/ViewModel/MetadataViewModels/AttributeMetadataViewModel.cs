using System.Runtime.InteropServices;
using Project.Model.Reflection.Model;
using ReflectionMVM.ViewModel;

namespace Project.ViewModel
{
    public class AttributeMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            return TypeName + " " + "[" + Name + "]";
        }

        #endregion

        #region Constructor

        internal AttributeMetadataViewModel(TypeMetadata attributeMetadata)
        {
            _attributeMetadata = attributeMetadata;
            Name = _attributeMetadata.TypeName;
            TypeName = _attributeMetadata.TypeName;
        }

        #endregion

        #region Private

        private readonly TypeMetadata _attributeMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey(_attributeMetadata.TypeName))
            {
                Child.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_attributeMetadata.TypeName]));
            }
            else
            {
                Child.Add(new TypeMetadataViewModel(_attributeMetadata));
            }

            WasBuilt = true;
        }
    }
}