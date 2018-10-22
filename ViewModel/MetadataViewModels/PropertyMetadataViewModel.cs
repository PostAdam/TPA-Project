﻿using Model.Reflection;
using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class PropertyMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetAttributes(_propertyMetadata.PropertyAttributes);
            fullname += StringUtility.GetMethodModifiers(_propertyMetadata.Modifiers);
            fullname += _propertyMetadata.TypeMetadata.TypeName;
            string getter = string.IsNullOrEmpty(_propertyMetadata.Getter?.Name)
                ? null
                : _propertyMetadata.Getter.Name + "; ";
            string setter = string.IsNullOrEmpty(_propertyMetadata.Getter?.Name)
                ? null
                : _propertyMetadata.Getter.Name + "; ";
            fullname += _propertyMetadata.Name;
            fullname += " { " + getter + setter + "}";
            return fullname;
        }

        #endregion

        #region Constructor

        internal PropertyMetadataViewModel(PropertyMetadata propertyMetadata)
        {
            _propertyMetadata = propertyMetadata;
        }

        #endregion

        #region Private

        private readonly PropertyMetadata _propertyMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();

            if (TypesDictionary.ReflectedTypes.ContainsKey(_propertyMetadata.TypeMetadata.FullName))
            {
                Children.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_propertyMetadata.TypeMetadata.FullName]));
            }
            else
            {
                Children.Add(new TypeMetadataViewModel(_propertyMetadata.TypeMetadata));
            }

            foreach (var attribute in _propertyMetadata.PropertyAttributes)
            {
                if (TypesDictionary.ReflectedTypes.ContainsKey(attribute.FullName))
                {
                    Children.Add(new AttributeMetadataViewModel(
                        TypesDictionary.ReflectedTypes[attribute.FullName]));
                }
                else
                {
                    Children.Add(new AttributeMetadataViewModel(attribute));
                }
            }

            if (_propertyMetadata.Getter != null)
            {
                Children.Add(new MethodMetadataViewModel(_propertyMetadata.Getter));
            }

            if (_propertyMetadata.Getter != null)
            {
                Children.Add(new MethodMetadataViewModel(_propertyMetadata.Setter));
            }


            WasBuilt = true;
        }
    }
}