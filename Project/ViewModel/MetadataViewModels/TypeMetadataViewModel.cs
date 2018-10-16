using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class TypeMetadataViewModel : MetadataViewModel
    {
        #region Constructor

        internal TypeMetadataViewModel( TypeMetadata type )
        {
            Name = type.TypeName;
            _type = type;
        }

        #endregion

        protected override void BuildMyself()
        {
            Child.Clear();

            if (_type.Methods != null)
                foreach (MethodMetadata method in _type.Methods)
                    Child.Add( new MethodMetadataViewModel( method ) );

            if (_type.Constructors != null)
                foreach (MethodMetadata constructors in _type.Constructors)
                    Child.Add( new MethodMetadataViewModel( constructors ) );

            if (_type.GenericArguments != null)
                foreach (TypeMetadata genericArgument in _type.GenericArguments)
                    Child.Add( new TypeMetadataViewModel( genericArgument ) );

            if (_type.ImplementedInterfaces != null)
                foreach (TypeMetadata implementedInterface in _type.ImplementedInterfaces)
                    Child.Add( new TypeMetadataViewModel( implementedInterface ) );

            if (_type.NestedTypes != null)
                foreach (TypeMetadata nestedType in _type.NestedTypes)
                    Child.Add( new TypeMetadataViewModel( nestedType ) );

            if (_type.Properties != null)
                foreach (PropertyMetadata property in _type.Properties)
                    Child.Add( new PropertyMetadataViewModel( property ) );

            if (_type.Fields != null)
                foreach (FieldMetadata field in _type.Fields)
                    Child.Add( new FieldMetadataViewModel( field ) );

            if (_type.Attributes != null)
                foreach (AttributeMetadata attribute in _type.Attributes)
                    Child.Add( new AttributeMetadataViewModel( attribute ) );

            WasBuilt = true;
        }

        private readonly TypeMetadata _type;
    }
}