using System.Collections.Generic;
using DataBaseSerializationSurrogates.MetadataSurrogates;
using ModelBase;

namespace DataBaseSerializationSurrogates
{
    public static class CollectionTypeAccessor
    {
        private static readonly ReproducedSurrogateTypes ReproducedSurrogateTypes = ReproducedSurrogateTypes.Instance;
        private static readonly ReproducedOriginalTypes ReproducedOriginalTypes = ReproducedOriginalTypes.Instance;

        public static ICollection<NamespaceMetadataSurrogate> GetNamespacesMetadata(
            IEnumerable<NamespaceMetadataBase> namespaces )
        {
            if ( namespaces == null )
            {
                return null;
            }

            ReproducedSurrogateTypes.Clear();
            ReproducedOriginalTypes.Clear();

            ICollection<NamespaceMetadataSurrogate> surrogates = new List<NamespaceMetadataSurrogate>();
            foreach ( NamespaceMetadataBase namespaceMetadata in namespaces )
            {
                surrogates.Add( new NamespaceMetadataSurrogate( namespaceMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<FieldMetadataSurrogate> GetFieldsMetadata(
            IEnumerable<FieldMetadataBase> fields )
        {
            if ( fields == null )
            {
                return null;
            }

            ICollection<FieldMetadataSurrogate> surrogates = new List<FieldMetadataSurrogate>();
            foreach ( FieldMetadataBase fieldMetadata in fields )
            {
                surrogates.Add( new FieldMetadataSurrogate( fieldMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<EventMetadataSurrogate> GetEventsMetadata(
            IEnumerable<EventMetadataBase> events )
        {
            if ( events == null )
            {
                return null;
            }

            ICollection<EventMetadataSurrogate> surrogates = new List<EventMetadataSurrogate>();
            foreach ( EventMetadataBase eventMetadata in events )
            {
                surrogates.Add( new EventMetadataSurrogate( eventMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<MethodMetadataSurrogate> GetMethodsMetadata(
            IEnumerable<MethodMetadataBase> methods )
        {
            if ( methods == null )
            {
                return null;
            }

            ICollection<MethodMetadataSurrogate> surrogates = new List<MethodMetadataSurrogate>();
            foreach ( MethodMetadataBase methodMetadata in methods )
            {
                surrogates.Add( new MethodMetadataSurrogate( methodMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<PropertyMetadataSurrogate> GetPropertiesMetadata(
            IEnumerable<PropertyMetadataBase> properties )
        {
            if ( properties == null )
            {
                return null;
            }

            ICollection<PropertyMetadataSurrogate> surrogates = new List<PropertyMetadataSurrogate>();
            foreach ( PropertyMetadataBase propertyMetadata in properties )
            {
                surrogates.Add( new PropertyMetadataSurrogate( propertyMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            if ( types == null )
            {
                return null;
            }

            ICollection<TypeMetadataSurrogate> surrogates = new List<TypeMetadataSurrogate>();
            foreach ( TypeMetadataBase typeMetadata in types )
            {
                surrogates.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return surrogates;
        }

        public static ICollection<ParameterMetadataSurrogate> GetParametersMetadata(
            IEnumerable<ParameterMetadataBase> parameters )
        {
            if ( parameters == null )
            {
                return null;
            }

            ICollection<ParameterMetadataSurrogate> surrogates = new List<ParameterMetadataSurrogate>();
            foreach ( ParameterMetadataBase parameterMetadata in parameters )
            {
                surrogates.Add( new ParameterMetadataSurrogate( parameterMetadata ) );
            }

            return surrogates;
        }
    }
}