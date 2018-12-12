using System.Collections.Generic;
using DataBaseSerializationSurrogates.MetadataSurrogates;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates
{
    public static class CollectionTypeAccessor
    {
        public static ICollection<NamespaceMetadataSurrogate> GetNamespacesMetadata(
            IEnumerable<NamespaceMetadata> namespaces )
        {
            if ( namespaces == null )
            {
                return null;
            }

            ICollection<NamespaceMetadataSurrogate> surrogates = new List<NamespaceMetadataSurrogate>();
            foreach ( NamespaceMetadata namespaceMetadata in namespaces )
            {
                surrogates.Add( new NamespaceMetadataSurrogate( namespaceMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<FieldMetadataSurrogate> GetFieldsMetadata(
            IEnumerable<FieldMetadata> fields )
        {
            if ( fields == null )
            {
                return null;
            }

            ICollection<FieldMetadataSurrogate> surrogates = new List<FieldMetadataSurrogate>();
            foreach ( FieldMetadata fieldMetadata in fields )
            {
                surrogates.Add( new FieldMetadataSurrogate( fieldMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<EventMetadataSurrogate> GetEventsMetadata(
            IEnumerable<EventMetadata> events )
        {
            if ( events == null )
            {
                return null;
            }

            ICollection<EventMetadataSurrogate> surrogates = new List<EventMetadataSurrogate>();
            foreach ( EventMetadata eventMetadata in events )
            {
                surrogates.Add( new EventMetadataSurrogate( eventMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<MethodMetadataSurrogate> GetMethodsMetadata(
            IEnumerable<MethodMetadata> methods )
        {
            if ( methods == null )
            {
                return null;
            }

            ICollection<MethodMetadataSurrogate> surrogates = new List<MethodMetadataSurrogate>();
            foreach ( MethodMetadata methodMetadata in methods )
            {
                surrogates.Add( new MethodMetadataSurrogate( methodMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<PropertyMetadataSurrogate> GetPropertiesMetadata(
            IEnumerable<PropertyMetadata> properties )
        {
            if ( properties == null )
            {
                return null;
            }

            ICollection<PropertyMetadataSurrogate> surrogates = new List<PropertyMetadataSurrogate>();
            foreach ( PropertyMetadata propertyMetadata in properties )
            {
                surrogates.Add( new PropertyMetadataSurrogate( propertyMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            if ( types == null )
            {
                return null;
            }

            ICollection<TypeMetadataSurrogate> surrogates = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                surrogates.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<ParameterMetadataSurrogate> GetParametersMetadata(
            IEnumerable<ParameterMetadata> parameters )
        {
            if ( parameters == null )
            {
                return null;
            }

            ICollection<ParameterMetadataSurrogate> surrogates = new List<ParameterMetadataSurrogate>();
            foreach ( ParameterMetadata parameterMetadata in parameters )
            {
                surrogates.Add( new ParameterMetadataSurrogate( parameterMetadata ) );
            }

            return surrogates;
        }
    }
}