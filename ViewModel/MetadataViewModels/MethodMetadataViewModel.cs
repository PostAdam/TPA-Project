using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model.Reflection;
using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class MethodMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Public

        // Properties
        public TypeMetadata ReturnType { get; }
        public IEnumerable<ParameterMetadata> Parameters { get; }

        // Methods
        public override string ToString()
        {
            return CrateName();
        }

        #endregion

        #region Constructor

        internal MethodMetadataViewModel(MethodMetadata methodMetadata)
        {
            _methodMetadata = methodMetadata;
            Name = _methodMetadata.Name;
            Parameters = _methodMetadata.Parameters;
            Modifier = GetModifierName(_methodMetadata.Modifiers?.Item1);
            ReturnType = _methodMetadata?.ReturnType;
        }

        #endregion

        #region Private

        private readonly MethodMetadata _methodMetadata;

        private string CrateName()
        {
            string modifierName = Modifier;
            if (modifierName != null)
            {
                modifierName += " ";
            }

            string parametersNames = GetParameterNames();
            string returnTypeName = _methodMetadata.ReturnType?.TypeName;
            string methodName = _methodMetadata.Name;
            return $"{modifierName}{methodName}({parametersNames}): {returnTypeName}";
        }

        private string GetParameterNames()
        {
            if (!Parameters.Any())
            {
                return "";
            }

            StringBuilder names = new StringBuilder();
            var last = Parameters.Last();
            foreach (var parameter in Parameters)
            {
                names.Append(" " + parameter.TypeMetadata.TypeName + " " + parameter.Name);
                if (parameter != last)
                {
                    names.Append(", ");
                }
                else
                {
                    names.Append(" ");
                }
            }

            return names.ToString();
        }

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();
            foreach (ParameterMetadata parameter in _methodMetadata.Parameters)
            {
                Child.Add(new ParameterMetadataViewModel(parameter));
            }

            if (_methodMetadata.ReturnType != null)
                if (TypesDictionary.ReflectedTypes.ContainsKey(_methodMetadata.ReturnType.TypeName))
                {
                    Child.Add(new TypeMetadataViewModel(
                        TypesDictionary.ReflectedTypes[_methodMetadata.ReturnType.TypeName]));
                }
                else
                {
                    Child.Add(new TypeMetadataViewModel(_methodMetadata.ReturnType));
                }

            foreach (var attribute in _methodMetadata.MethodAttributes)
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