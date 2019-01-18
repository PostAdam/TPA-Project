using System.Collections.Generic;
using System.Linq;
using DataBaseSerializationSurrogates.MetadataSurrogates;
using ModelBase;

namespace DataBaseSerializationSurrogates
{
    public static class CollectionOriginalTypeAccessor
    {
        public static IEnumerable<FieldMetadataBase> GetOriginalFieldsMetadata( IEnumerable<FieldMetadataSurrogate> fields )
        {
            return fields?.Select( f => f.GetOriginalFieldMetadata() );
        }

        public static IEnumerable<TypeMetadataBase> GetOriginalTypesMetadata( IEnumerable<TypeMetadataSurrogate> types )
        {
            return types?.Select( t => t.EmitOriginalTypeMetadata() );
        }

        public static IEnumerable<PropertyMetadataBase> GetOriginalPropertiesMetadata(
            IEnumerable<PropertyMetadataSurrogate> properties )
        {
            return properties?.Select( p => p.GetOriginalPropertyMetadata() );
        }

        public static IEnumerable<MethodMetadataBase> GetOriginalMethodsMetadata(
            IEnumerable<MethodMetadataSurrogate> methods )
        {
            return methods?.Select( m => m.GetOriginalMethodMetadata() );
        }

        public static IEnumerable<EventMetadataBase> GetOriginalEventsMetadata( IEnumerable<EventMetadataSurrogate> events )
        {
            return events?.Select( e => e.GetOriginalEventMetadata() );
        }

        public static IEnumerable<ParameterMetadataBase> GetOriginalParametersMetadata(
            IEnumerable<ParameterMetadataSurrogate> parameters )
        {
            return parameters?.Select( p => p.GetOriginalParameterMetadata() );
        }
    }
}