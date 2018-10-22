using Model.Reflection;
using Model.Reflection.MetadataModels;
using ViewModel.MetadataBaseViewModels;

namespace ViewModel.MetadataViewModels
{
    public class EventMetadataViewModel : MetadataBaseViewModel
    {
        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetAttributes(_eventMetadata.EventAttributes);
            fullname += _eventMetadata.Multicast ? "multicast " : "singlecast";
            fullname += _eventMetadata.TypeMetadata.TypeName + " ";
            fullname += _eventMetadata.Name;

            return fullname;
        }

        #region Constructor

        internal EventMetadataViewModel(EventMetadata eventMetadata)
        {
            _eventMetadata = eventMetadata;
        }

        #endregion

        private readonly EventMetadata _eventMetadata;

        protected override void BuildMyself()
        {
            Children.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey(_eventMetadata.TypeMetadata.FullName))
            {
                Children.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_eventMetadata.TypeMetadata.FullName]));
            }
            else
            {
                Children.Add(new TypeMetadataViewModel(_eventMetadata.TypeMetadata));
            }

            foreach (var attribute in _eventMetadata.EventAttributes)
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

            if (_eventMetadata.AddMethodMetadata != null)
                Children.Add(new MethodMetadataViewModel(_eventMetadata.AddMethodMetadata));

            if (_eventMetadata.RaiseMethodMetadata != null)
                Children.Add(new MethodMetadataViewModel(_eventMetadata.RaiseMethodMetadata));

            if (_eventMetadata.RemoveMethodMetadata != null)
                Children.Add(new MethodMetadataViewModel(_eventMetadata.RemoveMethodMetadata));

            WasBuilt = true;
        }
    }
}