using System;
using System.Linq;
using Model.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class TypeMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetAttributes(_typeMetadata.Attributes);
            fullname += StringUtility.GetTypeModifiers(_typeMetadata.Modifiers);
            fullname += _typeMetadata.TypeKind.ToString().ToLower() + " ";
            fullname += _typeMetadata.TypeName;
            fullname += StringUtility.GetGenerics(_typeMetadata);
            fullname += GetBaseTypeAndInterfaces();

            return fullname;
        }

        #endregion

        #region Constructor

        public TypeMetadataViewModel(TypeMetadata typeMetadata)
        {
            _typeMetadata = typeMetadata;
        }

        #endregion

        #region Private

        private readonly TypeMetadata _typeMetadata;

        private string GetBaseTypeAndInterfaces()
        {
            string inherits = String.Empty;
            if (_typeMetadata.BaseType != null ||
                (_typeMetadata.ImplementedInterfaces != null && _typeMetadata.ImplementedInterfaces.Any()))
            {
                inherits += ": ";
                inherits += _typeMetadata.BaseType != null ? _typeMetadata.BaseType.TypeName : String.Empty;
                inherits += StringUtility.GetGenerics(_typeMetadata?.BaseType);
                if (_typeMetadata.BaseType != null) inherits += ", ";
                inherits += GetImplementedInterfaces();
            }

            return inherits;
        }

        private string GetImplementedInterfaces()
        {
            string interfaces = String.Empty;
            if (_typeMetadata.ImplementedInterfaces != null && _typeMetadata.ImplementedInterfaces.Any())
            {
                interfaces += "";
                foreach (var implementedInterface in _typeMetadata.ImplementedInterfaces)
                {
                    interfaces += implementedInterface.TypeName;
                    interfaces += StringUtility.GetGenerics(implementedInterface);
                    if (implementedInterface != _typeMetadata.ImplementedInterfaces.Last())
                    {
                        interfaces += ", ";
                    }
                }
            }

            return interfaces;
        }

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();

            if (_typeMetadata.Attributes != null)
                foreach (TypeMetadata attribute in _typeMetadata.Attributes)
                    Children.Add(new AttributeMetadataViewModel(attribute));

            if (_typeMetadata.ImplementedInterfaces != null)
                foreach (TypeMetadata implementedInterface in _typeMetadata.ImplementedInterfaces)
                    Children.Add(new TypeMetadataViewModel(implementedInterface));

            if (_typeMetadata.Constructors != null)
                foreach (MethodMetadata constructors in _typeMetadata.Constructors)
                    Children.Add(new MethodMetadataViewModel(constructors));

            if (_typeMetadata.NestedTypes != null)
                foreach (TypeMetadata nestedType in _typeMetadata.NestedTypes)
                    Children.Add(new TypeMetadataViewModel(nestedType));

            if (_typeMetadata.Methods != null)
                foreach (MethodMetadata method in _typeMetadata.Methods)
                    Children.Add(new MethodMetadataViewModel(method));

            if (_typeMetadata.Properties != null)
                foreach (PropertyMetadata property in _typeMetadata.Properties)
                    Children.Add(new PropertyMetadataViewModel(property));

            if (_typeMetadata.Fields != null)
                foreach (FieldMetadata field in _typeMetadata.Fields)
                    Children.Add(new FieldMetadataViewModel(field));

            if (_typeMetadata.Events != null)
                foreach (EventMetadata singleEvent in _typeMetadata.Events)
                    Children.Add(new EventMetadataViewModel(singleEvent));

            WasBuilt = true;
        }
    }
}