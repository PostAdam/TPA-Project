using System.Collections.Generic;
using Model.Reflection.MetadataModels;
using XmlSerializationSurrogates.MetadataSurrogates;

namespace XmlSerializationSurrogates
{
    public static class CollectionTypeAccessor
    {
        public static IEnumerable<NamespaceMetadataSurrogate> GetNamespacesMetadata(
            IEnumerable<NamespaceMetadata> namespaces )
        {
            if ( namespaces == null )
            {
                return null;
            }

            List<NamespaceMetadataSurrogate> namespaceMetadatasSurrogate = new List<NamespaceMetadataSurrogate>();
            foreach ( NamespaceMetadata namespaceMetadata in namespaces )
            {
                namespaceMetadatasSurrogate.Add( new NamespaceMetadataSurrogate( namespaceMetadata ) );
            }

            return namespaceMetadatasSurrogate;
        }

        public static IEnumerable<FieldMetadataSurrogate> GetFieldsMetadata(
            IEnumerable<FieldMetadata> fields )
        {
            if ( fields == null )
            {
                return null;
            }

            List<FieldMetadataSurrogate> fliedMetadatasSurrogate = new List<FieldMetadataSurrogate>();
            foreach ( FieldMetadata fieldMetadata in fields )
            {
                fliedMetadatasSurrogate.Add( new FieldMetadataSurrogate( fieldMetadata ) );
            }

            return fliedMetadatasSurrogate;
        }

        public static IEnumerable<EventMetadataSurrogate> GetEventsMetadata(
            IEnumerable<EventMetadata> events )
        {
            if ( events == null )
            {
                return null;
            }

            List<EventMetadataSurrogate> eventMetadatasSurrogate = new List<EventMetadataSurrogate>();
            foreach ( EventMetadata eventMetadata in events )
            {
                eventMetadatasSurrogate.Add( new EventMetadataSurrogate( eventMetadata ) );
            }

            return eventMetadatasSurrogate;
        }

        public static IEnumerable<MethodMetadataSurrogate> GetMethodsMetadata(
            IEnumerable<MethodMetadata> methods )
        {
            if ( methods == null )
            {
                return null;
            }

            List<MethodMetadataSurrogate> methodMetadatasSurrogate = new List<MethodMetadataSurrogate>();
            foreach ( MethodMetadata methodMetadata in methods )
            {
                methodMetadatasSurrogate.Add( new MethodMetadataSurrogate( methodMetadata ) );
            }

            return methodMetadatasSurrogate;
        }

        public static IEnumerable<PropertyMetadataSurrogate> GetPropertiesMetadata(
            IEnumerable<PropertyMetadata> properties )
        {
            if ( properties == null )
            {
                return null;
            }

            List<PropertyMetadataSurrogate> propertyMetadatasSurrogate = new List<PropertyMetadataSurrogate>();
            foreach ( PropertyMetadata propertyMetadata in properties )
            {
                propertyMetadatasSurrogate.Add( new PropertyMetadataSurrogate( propertyMetadata ) );
            }

            return propertyMetadatasSurrogate;
        }

        private static readonly ReproducedSurrogateTypes _reproducedTypes = ReproducedSurrogateTypes.Instance;

        public static IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            if ( types == null )
            {
                return null;
            }

            List<TypeMetadataSurrogate> typeMetadatasSurrogate = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                typeMetadatasSurrogate.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata )/*new TypeMetadata( typeMetadata )*/ );
            }

            return typeMetadatasSurrogate;
        }

        public static IEnumerable<ParameterMetadataSurrogate> GetParametersMetadata(
            IEnumerable<ParameterMetadata> parameter )
        {
            if ( parameter == null )
            {
                return null;
            }

            List<ParameterMetadataSurrogate> parameterMetadatasSurrogate = new List<ParameterMetadataSurrogate>();
            foreach ( ParameterMetadata parameterMetadata in parameter )
            {
                parameterMetadatasSurrogate.Add( new ParameterMetadataSurrogate( parameterMetadata ) );
            }

            return parameterMetadatasSurrogate;
        }
    }
}