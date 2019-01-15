using Model.ModelDTG;
using Model.Reflection;

namespace ViewModel.MetadataViewModels
{
    public class EventMetadataViewModel : MetadataBaseViewModel
    {
        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;

        public override string ToString()
        {
            string fullname = "";
            fullname += StringUtility.GetAttributes( _eventMetadata.EventAttributes );
            fullname += _eventMetadata.Multicast ? "multicast " : "singlecast";
            fullname += _eventMetadata.TypeMetadata.TypeName + " ";
            fullname += _eventMetadata.Name;

            return fullname;
        }

        #region Constructor

        internal EventMetadataViewModel( EventMetadata eventMetadata )
        {
            _eventMetadata = eventMetadata;
        }

        #endregion

        private readonly EventMetadata _eventMetadata;

        protected override void BuildMyself()
        {
            Children.Clear();
            if ( _reflectedTypes.ContainsKey( _eventMetadata.TypeMetadata.FullName ) )
            {
                Children.Add( new TypeMetadataViewModel(
                    _reflectedTypes[ _eventMetadata.TypeMetadata.FullName ] ) );
            }
            else
            {
                Children.Add( new TypeMetadataViewModel( _eventMetadata.TypeMetadata ) );
            }

            foreach ( TypeMetadata attribute in _eventMetadata.EventAttributes )
            {
                if ( _reflectedTypes.ContainsKey( attribute.FullName ) )
                {
                    Children.Add( new AttributeMetadataViewModel(
                        _reflectedTypes[ attribute.FullName ] ) );
                }
                else
                {
                    Children.Add( new AttributeMetadataViewModel( attribute ) );
                }
            }

            if ( _eventMetadata.AddMethodMetadata != null )
                Children.Add( new MethodMetadataViewModel( _eventMetadata.AddMethodMetadata ) );

            if ( _eventMetadata.RaiseMethodMetadata != null )
                Children.Add( new MethodMetadataViewModel( _eventMetadata.RaiseMethodMetadata ) );

            if ( _eventMetadata.RemoveMethodMetadata != null )
                Children.Add( new MethodMetadataViewModel( _eventMetadata.RemoveMethodMetadata ) );

            WasBuilt = true;
        }
    }
}