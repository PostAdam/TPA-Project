using System.Collections.Generic;
using System.Linq;
using DataBaseSerializationSurrogates.MetadataSurrogates;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates
{
    public static class CollectionOriginalTypeAccessor
    {
        public static IEnumerable<FieldMetadata> GetOriginalFieldsMetadata( IEnumerable<FieldMetadataSurrogate> fields )
        {
            return fields?.Select( f => f.GetOriginalFieldMetadata() );
        }

        public static IEnumerable<TypeMetadata> GetOriginalTypesMetadata( IEnumerable<TypeMetadataSurrogate> types )
        {
            return types?.Select( t => t.EmitOriginalTypeMetadata() );
        }

        public static IEnumerable<PropertyMetadata> GetOriginalPropertiesMetadata(
            IEnumerable<PropertyMetadataSurrogate> properties )
        {
            return properties?.Select( p => p.GetOriginalPropertyMetadata() );
        }

        public static IEnumerable<MethodMetadata> GetOriginalMethodsMetadata(
            IEnumerable<MethodMetadataSurrogate> methods )
        {
            return methods?.Select( m => m.GetOriginalMethodMetadata() );
        }

        public static IEnumerable<EventMetadata> GetOriginalEventsMetadata( IEnumerable<EventMetadataSurrogate> events )
        {
            return events?.Select( e => e.GetOriginalEventMetadata() );
        }

        public static IEnumerable<ParameterMetadata> GetOriginalParametersMetadata(
            IEnumerable<ParameterMetadataSurrogate> parameters )
        {
            return parameters?.Select( p => p.GetOriginalParameterMetadata() );
        }
    }
}