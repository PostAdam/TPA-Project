using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class PropertyMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal PropertyMetadataViewModel( PropertyMetadata propertyMetadata )
        {
            Name = propertyMetadata.Name;
            _propertyMetadata = propertyMetadata;
        }

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();

            if (TypesDictionary.ReflectedTypes.ContainsKey( _propertyMetadata.TypeMetadata.TypeName ))
            {
                Child.Add( new TypeMetadataViewModel(
                    TypesDictionary.ReflectedTypes[_propertyMetadata.TypeMetadata.TypeName] ) );
            }
            else
            {
                Child.Add( new TypeMetadataViewModel( _propertyMetadata.TypeMetadata ) );
            }

            WasBuilt = true;
        }

        private readonly PropertyMetadata _propertyMetadata;
    }
}