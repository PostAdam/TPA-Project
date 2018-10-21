using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class TypeMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string modifier = string.IsNullOrEmpty(Modifier) ? Modifier : Modifier + " ";
            return modifier + Name;
        }

        #endregion

        #region Constructor

        public TypeMetadataViewModel(TypeMetadata typeMetadataMetadata)
        {
            _typeMetadata = typeMetadataMetadata;
            Modifier = GetModifierName(_typeMetadata.Modifiers?.Item1);
            Name = _typeMetadata.TypeName;
        }

        #endregion

        #region Private

        private readonly TypeMetadata _typeMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();

            if (_typeMetadata.Methods != null)
                foreach (MethodMetadata method in _typeMetadata.Methods)
                    Child.Add(new MethodMetadataViewModel(method));

            if (_typeMetadata.Constructors != null)
                foreach (MethodMetadata constructors in _typeMetadata.Constructors)
                    Child.Add(new MethodMetadataViewModel(constructors));

            if (_typeMetadata.GenericArguments != null)
                foreach (TypeMetadata genericArgument in _typeMetadata.GenericArguments)
                    Child.Add(new TypeMetadataViewModel(genericArgument));

            if (_typeMetadata.ImplementedInterfaces != null)
                foreach (TypeMetadata implementedInterface in _typeMetadata.ImplementedInterfaces)
                    Child.Add(new TypeMetadataViewModel(implementedInterface));

            if (_typeMetadata.NestedTypes != null)
                foreach (TypeMetadata nestedType in _typeMetadata.NestedTypes)
                    Child.Add(new TypeMetadataViewModel(nestedType));

            if (_typeMetadata.Properties != null)
                foreach (PropertyMetadata property in _typeMetadata.Properties)
                    Child.Add(new PropertyMetadataViewModel(property));

            if (_typeMetadata.Fields != null)
                foreach (FieldMetadata field in _typeMetadata.Fields)
                    Child.Add(new FieldMetadataViewModel(field));

            if (_typeMetadata.Attributes != null)
                foreach (TypeMetadata attribute in _typeMetadata.Attributes)
                    Child.Add(new AttributeMetadataViewModel(attribute));

            if (_typeMetadata.Events != null)
                foreach (EventMetadata singleEvent in _typeMetadata.Events)
                    Child.Add(new EventMetadataViewModel(singleEvent));

            WasBuilt = true;
        }
    }
}