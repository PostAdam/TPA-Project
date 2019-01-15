using System.Collections.Generic;
using System.Linq;
using ModelBase;

namespace Model.ModelDTG.Accessors
{
    public static class CollectionTypeAccessor
    {
        public static IEnumerable<NamespaceMetadata> GetNamespacesMetadata(
            IEnumerable<NamespaceMetadataBase> namespaces )
        {
            return namespaces?.Select( n => new NamespaceMetadata( n ) );
        }

        public static IEnumerable<FieldMetadata> GetFieldsMetadata(
            IEnumerable<FieldMetadataBase> fields )
        {
            return fields?.Select( f => new FieldMetadata( f ) );
        }

        public static IEnumerable<EventMetadata> GetEventsMetadata(
            IEnumerable<EventMetadataBase> events )
        {
            return events?.Select( e => new EventMetadata( e ) );
        }

        public static IEnumerable<MethodMetadata> GetMethodsMetadata(
            IEnumerable<MethodMetadataBase> methods )
        {
            return methods?.Select( m => new MethodMetadata( m ) );
        }

        public static IEnumerable<PropertyMetadata> GetPropertiesMetadata(
            IEnumerable<PropertyMetadataBase> properties )
        {
            return properties?.Select( p => new PropertyMetadata( p ) );
        }

        public static IEnumerable<TypeMetadata> GetTypesMetadata( IEnumerable<TypeMetadataBase> types )
        {
            return types?.Select( TypeMetadata.EmitSurrogateTypeMetadata );
        }

        public static IEnumerable<ParameterMetadata> GetParametersMetadata(
            IEnumerable<ParameterMetadataBase> parameter )
        {
            return parameter?.Select( p => new ParameterMetadata( p ) );
        }
    }
}