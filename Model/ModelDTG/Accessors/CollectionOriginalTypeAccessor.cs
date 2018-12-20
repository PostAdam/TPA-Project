using System.Collections.Generic;
using System.Linq;
using ModelBase;

namespace Model.ModelDTG.Accessors
{
    public static class CollectionOriginalTypeAccessor
    {
        public static IEnumerable<FieldMetadataBase> GetOriginalFieldsMetadata( IEnumerable<FieldMetadata> fields )
        {
            return fields?.Select( f => f.GetOriginalFieldMetadata() );
        }

        public static IEnumerable<TypeMetadataBase> GetOriginalTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            return types?.Select( t => t.EmitOriginalTypeMetadata() );
        }

        public static IEnumerable<PropertyMetadataBase> GetOriginalPropertiesMetadata(
            IEnumerable<PropertyMetadata> properties )
        {
            return properties?.Select( p => p.GetOriginalPropertyMetadata() );
        }

        public static IEnumerable<MethodMetadataBase> GetOriginalMethodsMetadata(
            IEnumerable<MethodMetadata> methods )
        {
            return methods?.Select( m => m.GetOriginalMethodMetadata() );
        }

        public static IEnumerable<EventMetadataBase> GetOriginalEventsMetadata( IEnumerable<EventMetadata> events )
        {
            return events?.Select( e => e.GetOriginalEventMetadata() );
        }

        public static IEnumerable<ParameterMetadataBase> GetOriginalParametersMetadata(
            IEnumerable<ParameterMetadata> parameters )
        {
            return parameters?.Select( p => p.GetOriginalParameterMetadata() );
        }
    }
}