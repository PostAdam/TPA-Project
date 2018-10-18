/*using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class EventMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal EventMetadataViewModel( FieldMetadata fieldMetadata )
        {
            Name = GetModifier( fieldMetadata.TypeMetadata.Modifiers?.Item1 ) + " " +
                   fieldMetadata.TypeMetadata.TypeName + " " + fieldMetadata.Name;
            _fieldMetadata = fieldMetadata;
        }

        #endregion

        private readonly EventMetadata _eventMetadata;

        protected override void BuildMyself()
        {
            Child.Clear();
            if ( TypesDictionary.ReflectedTypes.ContainsKey( _fieldMetadata.TypeMetadata.TypeName ) )
            {
                Child.Add( new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[ _fieldMetadata.TypeMetadata.TypeName ] ) );
            }
            else
            {
                Child.Add( new TypeMetadataViewModel( _fieldMetadata.TypeMetadata ) );
            }

            WasBuilt = true;
        }
    }
}*/