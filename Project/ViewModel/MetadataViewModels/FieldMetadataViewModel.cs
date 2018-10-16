using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class FieldMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal FieldMetadataViewModel( FieldMetadata fieldMetadata )
        {
            Name = fieldMetadata.Name;
            _fieldMetadata = fieldMetadata;
        }

        #endregion

        private readonly FieldMetadata _fieldMetadata;

        protected override void BuildMyself()
        {
            Child.Clear();
            if (TypesDictionary.ReflectedTypes.ContainsKey( _fieldMetadata.TypeMetadata.TypeName ))
            {
                Child.Add( new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_fieldMetadata.TypeMetadata.TypeName] ) );
            }
            else
            {
                Child.Add( new TypeMetadataViewModel( _fieldMetadata.TypeMetadata ) );
            }

            WasBuilt = true;
        }
    }
}