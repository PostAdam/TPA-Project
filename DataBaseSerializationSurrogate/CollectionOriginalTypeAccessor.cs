using System.Collections.Generic;
using DataBaseSerializationSurrogate.MetadataSurrogates;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogate
{
    public static class CollectionOriginalTypeAccessor
    {
        public static IEnumerable<FieldMetadata> GetOriginalFieldsMetadata( IEnumerable<FieldMetadataSurrogate> fields )
        {
            if ( fields == null )
            {
                return null;
            }

            List<FieldMetadata> originalFields = new List<FieldMetadata>();
            foreach ( FieldMetadataSurrogate fieldMetadataSurrogate in fields )
            {
                originalFields.Add( fieldMetadataSurrogate.GetOriginalFieldMetadata() );
            }

            return originalFields;
        }

        public static IEnumerable<TypeMetadata> GetOriginalTypesMetadata( IEnumerable<TypeMetadataSurrogate> types )
        {
            if ( types == null )
            {
                return null;
            }

            List<TypeMetadata> originalTypes = new List<TypeMetadata>();
            foreach ( TypeMetadataSurrogate typeMetadataSurrogate in types )
            {
                originalTypes.Add( typeMetadataSurrogate.EmitOriginalTypeMetadata() );
            }

            return originalTypes;
        }

        public static IEnumerable<PropertyMetadata> GetOriginalPropertiesMetadata(
            IEnumerable<PropertyMetadataSurrogate> properties )
        {
            if ( properties == null )
            {
                return null;
            }

            List<PropertyMetadata> originalProperties = new List<PropertyMetadata>();
            foreach ( PropertyMetadataSurrogate propertyMetadataSurrogate in properties )
            {
                originalProperties.Add( propertyMetadataSurrogate.GetOriginalPropertyMetadata() );
            }

            return originalProperties;
        }

        public static IEnumerable<MethodMetadata> GetOriginalMethodsMetadata(
            IEnumerable<MethodMetadataSurrogate> methods )
        {
            if ( methods == null )
            {
                return null;
            }

            List<MethodMetadata> originalMethods = new List<MethodMetadata>();
            foreach ( MethodMetadataSurrogate methodMetadataSurrogate in methods )
            {
                originalMethods.Add( methodMetadataSurrogate.GetOriginalMethodMetadata() );
            }

            return originalMethods;
        }

        public static IEnumerable<EventMetadata> GetOriginalEventsMetadata( IEnumerable<EventMetadataSurrogate> events )
        {
            if ( events == null )
            {
                return null;
            }

            List<EventMetadata> originalEvents = new List<EventMetadata>();
            foreach ( EventMetadataSurrogate eventMetadataSurrogate in events )
            {
                originalEvents.Add( eventMetadataSurrogate.GetOriginalEventMetadata() );
            }

            return originalEvents;
        }

        public static IEnumerable<ParameterMetadata> GetOriginalParametersMetadata(
            IEnumerable<ParameterMetadataSurrogate> parameters )
        {
            if ( parameters == null )
            {
                return null;
            }

            List<ParameterMetadata> originalParameters = new List<ParameterMetadata>();
            foreach ( ParameterMetadataSurrogate parameterMetadataSurrogate in parameters )
            {
                originalParameters.Add( parameterMetadataSurrogate.GetOriginalParameterMetadata() );
            }

            return originalParameters;
        }
    }
}