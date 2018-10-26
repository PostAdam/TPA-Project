using System;
using Model.Reflection;
using Model.Reflection.Enums;
using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class FieldMetadataViewModel : MetadataBaseViewModel
    {
        #region Public

        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetAttributes(_fieldMetadata.FieldAttributes);
            fullname += GetModifiers();
            fullname += _fieldMetadata.TypeMetadata.TypeName + " ";
            fullname += _fieldMetadata.Name;
            return fullname;
        }

        #endregion

        #region Constructor

        internal FieldMetadataViewModel(FieldMetadata fieldMetadata)
        {
            _fieldMetadata = fieldMetadata;
        }

        #endregion

        #region Private 

        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;

        private readonly FieldMetadata _fieldMetadata;

        #endregion

        protected override void BuildMyself()
        {
            Children.Clear();
            if ( _reflectedTypes.ContainsKey(_fieldMetadata.TypeMetadata.FullName))
            {
                Children.Add(new TypeMetadataViewModel(
                    _reflectedTypes[ _fieldMetadata.TypeMetadata.FullName]));
            }
            else
            {
                Children.Add(new TypeMetadataViewModel(_fieldMetadata.TypeMetadata));
            }

            foreach (TypeMetadata attribute in _fieldMetadata.FieldAttributes)
            {
                if ( _reflectedTypes.ContainsKey(attribute.FullName))
                {
                    Children.Add(new AttributeMetadataViewModel(
                        _reflectedTypes[ attribute.FullName]));
                }
                else
                {
                    Children.Add(new AttributeMetadataViewModel(attribute));
                }
            }

            WasBuilt = true;
        }

        private string GetModifiers()
        {
            if (_fieldMetadata.Modifiers == null) return String.Empty;

            string mods = String.Empty;
            mods += _fieldMetadata.Modifiers.Item1 + " ";
            mods += _fieldMetadata.Modifiers.Item2 == StaticEnum.Static
                ? _fieldMetadata.Modifiers.Item2 + " "
                : String.Empty;
            return mods.ToLower();
        }
    }
}