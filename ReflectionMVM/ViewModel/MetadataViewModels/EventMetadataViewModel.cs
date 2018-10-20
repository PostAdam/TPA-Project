using Project.Model.Reflection.Model;
using ReflectionMVM.ViewModel;

namespace Project.ViewModel
{
    public class EventMetadataViewModel : TypeMetadataBaseViewModel
    {
        #region Constructor

        internal EventMetadataViewModel(EventMetadata eventMetadata)
        {
            _eventMetadata = eventMetadata;
            Modifier = GetModifierName(_eventMetadata.Modifiers?.Item1);
            Name = _eventMetadata.Name;
            TypeName = _eventMetadata.TypeMetadata.TypeName;
            AddMethodMetadata = _eventMetadata.AddMethodMetadata;
            RaiseMethodMetadata = _eventMetadata.RaiseMethodMetadata;
            RemoveMethodMetadata = _eventMetadata.RemoveMethodMetadata;
        }

        #endregion

        private readonly EventMetadata _eventMetadata;
        public MethodMetadata AddMethodMetadata { get; }
        public MethodMetadata RaiseMethodMetadata { get; }
        public MethodMetadata RemoveMethodMetadata { get; }

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey(_eventMetadata.TypeMetadata.TypeName))
            {
                Child.Add(new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_eventMetadata.TypeMetadata.TypeName]));
            }
            else
            {
                Child.Add(new TypeMetadataViewModel(_eventMetadata.TypeMetadata));
            }

            foreach (var attribute in _eventMetadata.EventAttributes)
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

            if (AddMethodMetadata != null)
                Child.Add(new MethodMetadataViewModel(AddMethodMetadata));

            if (RaiseMethodMetadata != null)
                Child.Add(new MethodMetadataViewModel(RaiseMethodMetadata));

            if (RemoveMethodMetadata != null)
                Child.Add(new MethodMetadataViewModel(RemoveMethodMetadata));

            WasBuilt = true;
        }

        public override string ToString()
        {
            return TypeName + " " + Name;
        }
    }
}