using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class MethodMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal MethodMetadataViewModel( MethodMetadata methodMetadata )
        {
            Name = methodMetadata.Name;
            _methodMetadata = methodMetadata;
        }

        #endregion

        private readonly MethodMetadata _methodMetadata;

        protected override void BuildMyself()
        {
            Child.Clear();
            foreach (ParameterMetadata parameter in _methodMetadata.Parameters)
            {
                Child.Add( new ParameterMetadataViewModel( parameter ) );
            }

            WasBuilt = true;
        }
    }
}