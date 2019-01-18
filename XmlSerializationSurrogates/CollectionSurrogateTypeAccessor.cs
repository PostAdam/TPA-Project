using System.Collections.Generic;
using System.Linq;
using ModelBase;
using XmlSerializationSurrogates.MetadataSurrogates;

namespace XmlSerializationSurrogates
{
    public static class CollectionTypeAccessor
    {
        public static IEnumerable<NamespaceMetadataSurrogate> GetNamespacesMetadata(
            IEnumerable<NamespaceMetadataBase> namespaces )
        {
            return namespaces?.Select( n => new NamespaceMetadataSurrogate( n ) );
        }

        public static IEnumerable<FieldMetadataSurrogate> GetFieldsMetadata(
            IEnumerable<FieldMetadataBase> fields )
        {
            return fields?.Select( f => new FieldMetadataSurrogate( f ) );
        }

        public static IEnumerable<EventMetadataSurrogate> GetEventsMetadata(
            IEnumerable<EventMetadataBase> events )
        {
            return events?.Select( e => new EventMetadataSurrogate( e ) );
        }

        public static IEnumerable<MethodMetadataSurrogate> GetMethodsMetadata(
            IEnumerable<MethodMetadataBase> methods )
        {
            return methods?.Select( m => new MethodMetadataSurrogate( m ) );
        }

        public static IEnumerable<PropertyMetadataSurrogate> GetPropertiesMetadata(
            IEnumerable<PropertyMetadataBase> properties )
        {
            return properties?.Select( p => new PropertyMetadataSurrogate( p ) );
        }

        public static IEnumerable<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            return types?.Select( TypeMetadataSurrogate.EmitSurrogateTypeMetadata );
        }

        public static IEnumerable<ParameterMetadataSurrogate> GetParametersMetadata(
            IEnumerable<ParameterMetadataBase> parameter )
        {
            return parameter?.Select( p => new ParameterMetadataSurrogate( p ) );
        }
    }
}