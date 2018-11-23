using System.Collections.Generic;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.NewSurrogates
{
    public static class CollectionOryginalTypeAccessor
    {
        public static IEnumerable<FieldMetadata> GetOryginalFieldsMetadata( IEnumerable<FieldMetadataSurrogate> fields )
        {
            if ( fields == null )
            {
                return null;
            }

            List<FieldMetadata> oryginalFields = new List<FieldMetadata>();
            foreach ( FieldMetadataSurrogate fieldMetadataSurrogate in fields )
            {
                oryginalFields.Add( fieldMetadataSurrogate.GetOryginalFieldMetadata() );
            }

            return oryginalFields;
        }

        public static IEnumerable<TypeMetadata> GetOryginalTypesMetadata( IEnumerable<TypeMetadataSurrogate> types )
        {
            if ( types == null )
            {
                return null;
            }

            List<TypeMetadata> oryginalTypes = new List<TypeMetadata>();
            foreach ( TypeMetadataSurrogate typeMetadataSurrogate in types )
            {
                oryginalTypes.Add( typeMetadataSurrogate.EmitOriginalTypeMetadata() );
            }

            return oryginalTypes;
        }

        public static IEnumerable<PropertyMetadata> GetOryginalPropertiesMetadata(
            IEnumerable<PropertyMetadataSurrogate> properties )
        {
            if ( properties == null )
            {
                return null;
            }

            List<PropertyMetadata> oryginalProperties = new List<PropertyMetadata>();
            foreach ( PropertyMetadataSurrogate propertyMetadataSurrogate in properties )
            {
                oryginalProperties.Add( propertyMetadataSurrogate.GetOryginalPropertyMetadata() );
            }

            return oryginalProperties;
        }

        public static IEnumerable<MethodMetadata> GetOryginalMethodsMetadata(
            IEnumerable<MethodMetadataSurrogate> methods )
        {
            if ( methods == null )
            {
                return null;
            }

            List<MethodMetadata> oryginalMethods = new List<MethodMetadata>();
            foreach ( MethodMetadataSurrogate methodMetadataSurrogate in methods )
            {
                oryginalMethods.Add( methodMetadataSurrogate.GetOryginalMethodMetadata() );
            }

            return oryginalMethods;
        }

        public static IEnumerable<EventMetadata> GetOryginalEventsMetadata( IEnumerable<EventMetadataSurrogate> events )
        {
            if ( events == null )
            {
                return null;
            }

            List<EventMetadata> oryginalEvents = new List<EventMetadata>();
            foreach ( EventMetadataSurrogate eventMetadataSurrogate in events )
            {
                oryginalEvents.Add( eventMetadataSurrogate.GetOryginalEventMetadata() );
            }

            return oryginalEvents;
        }

        public static IEnumerable<ParameterMetadata> GetOryginalParametersMetadata(
            IEnumerable<ParameterMetadataSurrogate> parameters )
        {
            if ( parameters == null )
            {
                return null;
            }

            List<ParameterMetadata> oryginalParameters = new List<ParameterMetadata>();
            foreach ( ParameterMetadataSurrogate parameterMetadataSurrogate in parameters )
            {
                oryginalParameters.Add( parameterMetadataSurrogate.GetOryginalParameterMetadata() );
            }

            return oryginalParameters;
        }
    }
}