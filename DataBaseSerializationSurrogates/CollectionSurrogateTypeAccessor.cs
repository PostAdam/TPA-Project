using System.Collections.Generic;
using System.Linq;
using DataBaseSerializationSurrogates.MetadataSurrogates;
using Model.Reflection.MetadataModels;

namespace DataBaseSerializationSurrogates
{
    public static class CollectionTypeAccessor
    {
        public static ICollection<NamespaceMetadataSurrogate> GetNamespacesMetadata(
            IEnumerable<NamespaceMetadata> namespaces )
        {
            if ( namespaces == null )
            {
                return null;
            }
            ICollection< NamespaceMetadataSurrogate > surrogates = new List<NamespaceMetadataSurrogate>();
            IEnumerable<NamespaceMetadata> original = namespaces.ToList();

            foreach ( NamespaceMetadata namespaceMetadata in original )
            {
                surrogates.Add( new NamespaceMetadataSurrogate( namespaceMetadata ) );
            }

            return surrogates;
//            return namespaces?.Select( n => new NamespaceMetadataSurrogate( n ) );
        }

        public static ICollection<FieldMetadataSurrogate> GetFieldsMetadata(
            IEnumerable<FieldMetadata> fields )
        {
            if ( fields == null )
            {
                return null;
            }
            ICollection<FieldMetadataSurrogate> surrogates = new List<FieldMetadataSurrogate>();
            IEnumerable<FieldMetadata> original = fields.ToList();

            foreach ( FieldMetadata fieldMetadata in original )
            {
                surrogates.Add( new FieldMetadataSurrogate( fieldMetadata ) );
            }

            return surrogates;
            //return fields?.Select( f => new FieldMetadataSurrogate( f ) );
        }

        public static ICollection<EventMetadataSurrogate> GetEventsMetadata(
            IEnumerable<EventMetadata> events )
        {
            if ( events == null )
            {
                return null;
            }
            ICollection<EventMetadataSurrogate> surrogates = new List<EventMetadataSurrogate>();
            IEnumerable<EventMetadata> original = events.ToList();

            foreach ( EventMetadata eventMetadata in original )
            {
                surrogates.Add( new EventMetadataSurrogate( eventMetadata ) );
            }

            return surrogates;
//            return events?.Select( e => new EventMetadataSurrogate( e ) );
        }

        public static ICollection<MethodMetadataSurrogate> GetMethodsMetadata(
            IEnumerable<MethodMetadata> methods )
        {
            if ( methods == null )
            {
                return null;
            }
            ICollection<MethodMetadataSurrogate> surrogates = new List<MethodMetadataSurrogate>();
            IEnumerable<MethodMetadata> original = methods.ToList();

            foreach ( MethodMetadata methodMetadata in original )
            {
                surrogates.Add( new MethodMetadataSurrogate( methodMetadata ) );
            }

            return surrogates;
            //            return methods?.Select( m => new MethodMetadataSurrogate( m ) );
        }

        public static ICollection<PropertyMetadataSurrogate> GetPropertiesMetadata(
            IEnumerable<PropertyMetadata> properties )
        {
            if ( properties == null )
            {
                return null;
            }
            ICollection<PropertyMetadataSurrogate> surrogates = new List<PropertyMetadataSurrogate>();
            IEnumerable<PropertyMetadata> original = properties.ToList();

            foreach ( PropertyMetadata propertyMetadata in original )
            {
                surrogates.Add( new PropertyMetadataSurrogate( propertyMetadata ) );
            }

            return surrogates;
            //            return properties?.Select( p => new PropertyMetadataSurrogate( p ) );
        }

        public static ICollection<TypeMetadataSurrogate> GetTypesMetadata( IEnumerable<TypeMetadata> types )
        {
            if ( types == null )
            {
                return null;
            }
            ICollection<TypeMetadataSurrogate> surrogates = new List<TypeMetadataSurrogate>();
            IEnumerable<TypeMetadata> original = types.ToList();

            foreach ( TypeMetadata typeMetadata in original )
            {
                surrogates.Add( TypeMetadataSurrogate.EmitSurrogateTypeMetadata( typeMetadata ) );
            }

            return surrogates;
            //            return types?.Select( TypeMetadataSurrogate.EmitSurrogateTypeMetadata );
        }

        public static ICollection<ParameterMetadataSurrogate> GetParametersMetadata(
            IEnumerable<ParameterMetadata> parameters )
        {
            if ( parameters == null )
            {
                return null;
            }
            ICollection<ParameterMetadataSurrogate> surrogates = new List<ParameterMetadataSurrogate>();
            IEnumerable<ParameterMetadata> original = parameters.ToList();

            foreach ( ParameterMetadata parameterMetadata in original )
            {
                surrogates.Add( new ParameterMetadataSurrogate( parameterMetadata ) );
            }

            return surrogates;
            //            return parameter?.Select( p => new ParameterMetadataSurrogate( p ) );
        }
    }
}