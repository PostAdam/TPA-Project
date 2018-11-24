using System.Collections.Generic;
using System.Linq;
using DataBaseSerializationSurrogates.MetadataSurrogates;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates
{
    public static class CollectionTypeAccessor
    {
        public static IEnumerable<NamespaceMetadataSurrogate> GetNamespacesMetadata(
            IEnumerable<NamespaceMetadata> namespaces )
        {
            return namespaces?.Select( n => new NamespaceMetadataSurrogate( n ) );
        }

        public static IEnumerable<FieldMetadataSurrogate> GetFieldsMetadata(
            IEnumerable<FieldMetadata> fields )
        {
            return fields?.Select( f => new FieldMetadataSurrogate( f ) );
        }

        public static IEnumerable<EventMetadataSurrogate> GetEventsMetadata(
            IEnumerable<EventMetadata> events )
        {
            return events?.Select( e => new EventMetadataSurrogate( e ) );
        }

        public static IEnumerable<MethodMetadataSurrogate> GetMethodsMetadata(
            IEnumerable<MethodMetadata> methods )
        {
            return methods?.Select( m => new MethodMetadataSurrogate( m ) );
        }

        public static IEnumerable<PropertyMetadataSurrogate> GetPropertiesMetadata(
            IEnumerable<PropertyMetadata> properties )
        {
            return properties?.Select( p => new PropertyMetadataSurrogate( p ) );
        }

        public static IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            return types?.Select( TypeMetadataSurrogate.EmitSurrogateTypeMetadata );
        }

        public static IEnumerable<ParameterMetadataSurrogate> GetParametersMetadata(
            IEnumerable<ParameterMetadata> parameter )
        {
            return parameter?.Select( p => new ParameterMetadataSurrogate( p ) );
        }
    }
}