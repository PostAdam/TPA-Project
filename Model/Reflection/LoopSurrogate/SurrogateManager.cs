using System.Collections.Generic;
using System.Linq;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.LoopSurrogate
{
    public class SurrogateManager
    {
        #region Constructor

        public SurrogateManager( AssemblyMetadata assemblyMetadata )
        {
            GetOryginalMetadata( assemblyMetadata );
            CreateSurrogateModel();
        }

        private void GetOryginalMetadata( AssemblyMetadata assemblyMetadata )
        {
            AssemblyMetadata = assemblyMetadata;
            NamespaceMetadatas = assemblyMetadata.Namespaces;
            TypeMetadatas = GetTypes( NamespaceMetadatas );
            EventMetadatas = GetEvents( TypeMetadatas );
            FieldMetadatas = GetFields( TypeMetadatas );
            MethodMetadatas = GetMethods( TypeMetadatas );
            ParameterMetadatas = GetParameters( MethodMetadatas );
            PropertyMetadatas = GetProperties( TypeMetadatas );
        }

        private void CreateSurrogateModel()
        {
            AssemblyMetadataSurrogate = new AssemblyMetadataSurrogate( AssemblyMetadata );
            NamespaceMetadataSurrogates = SetNamespaceSurrogates( NamespaceMetadatas );
            TypeMetadataSurrogates = SetTypeSurrogates( TypeMetadatas );
            EventMetadataSurrogates = SetEventSurrogate( EventMetadatas );
            FieldMetadataSurrogates = SetFieldSurrogates( FieldMetadatas );
            MethodMetadataSurrogates = SetMethodSurrogates( MethodMetadatas );
            ParameterMetadataSurrogates = SetParameterSurrogates( ParameterMetadatas );
            PropertyMetadataSurrogates = SetPropertieSurrogates( PropertyMetadatas );
        }

        private IEnumerable<PropertyMetadataSurrogate> SetPropertieSurrogates(
            IEnumerable<PropertyMetadata> propertyMetadatas )
        {
            return propertyMetadatas.Select( p => new PropertyMetadataSurrogate( p ) );
        }

        private IEnumerable<ParameterMetadataSurrogate> SetParameterSurrogates(
            IEnumerable<ParameterMetadata> parameterMetadatas )
        {
            return parameterMetadatas.Select( p => new ParameterMetadataSurrogate( p ) );
        }

        private IEnumerable<MethodMetadataSurrogate> SetMethodSurrogates( IEnumerable<MethodMetadata> methodMetadatas )
        {
            return methodMetadatas.Select( m => new MethodMetadataSurrogate( m ) );
        }

        private IEnumerable<FieldMetadataSurrogate> SetFieldSurrogates( IEnumerable<FieldMetadata> fieldMetadatas )
        {
            return fieldMetadatas.Select( f => new FieldMetadataSurrogate( f ) );
        }

        private IEnumerable<EventMetadataSurrogate> SetEventSurrogate( IEnumerable<EventMetadata> eventMetadatas )
        {
            return eventMetadatas.Select( e => new EventMetadataSurrogate( e ) );
        }

        private IEnumerable<TypeMetadataSurrogate> SetTypeSurrogates( IEnumerable<TypeMetadata> typeMetadatas )
        {
            return typeMetadatas.Select( t => new TypeMetadataSurrogate( t ) );
        }

        private IEnumerable<NamespaceMetadataSurrogate> SetNamespaceSurrogates(
            IEnumerable<NamespaceMetadata> namespaceMetadatas )
        {
            return namespaceMetadatas.Select( n => new NamespaceMetadataSurrogate( n ) );
        }

        #endregion

        #region Properties

        public AssemblyMetadata AssemblyMetadata { get; set; }
        public IEnumerable<NamespaceMetadata> NamespaceMetadatas { get; set; }
        public IEnumerable<TypeMetadata> TypeMetadatas { get; set; }
        public IEnumerable<EventMetadata> EventMetadatas { get; set; }
        public IEnumerable<FieldMetadata> FieldMetadatas { get; set; }
        public IEnumerable<MethodMetadata> MethodMetadatas { get; set; }
        public IEnumerable<ParameterMetadata> ParameterMetadatas { get; set; }
        public IEnumerable<PropertyMetadata> PropertyMetadatas { get; set; }

        public AssemblyMetadataSurrogate AssemblyMetadataSurrogate { get; set; }
        public IEnumerable<NamespaceMetadataSurrogate> NamespaceMetadataSurrogates { get; set; }
        public IEnumerable<TypeMetadataSurrogate> TypeMetadataSurrogates { get; set; }
        public IEnumerable<EventMetadataSurrogate> EventMetadataSurrogates { get; set; }
        public IEnumerable<FieldMetadataSurrogate> FieldMetadataSurrogates { get; set; }
        public IEnumerable<MethodMetadataSurrogate> MethodMetadataSurrogates { get; set; }
        public IEnumerable<ParameterMetadataSurrogate> ParameterMetadataSurrogates { get; set; }
        public IEnumerable<PropertyMetadataSurrogate> PropertyMetadataSurrogates { get; set; }

        #endregion

        #region Properties Resolvers

        private IEnumerable<PropertyMetadata> GetProperties( IEnumerable<TypeMetadata> types )
        {
            List<PropertyMetadata> properties = new List<PropertyMetadata>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                foreach ( PropertyMetadata propertyMetadata in typeMetadata.Properties )
                {
                    if ( properties.Count( p => p.Name == propertyMetadata.Name ) < 1 )
                    {
                        properties.Add( propertyMetadata );
                    }
                }
            }

            return properties;
        }

        private IEnumerable<ParameterMetadata> GetParameters( IEnumerable<MethodMetadata> methods )
        {
            List<ParameterMetadata> parameters = new List<ParameterMetadata>();
            foreach ( MethodMetadata methodMetadata in methods )
            {
                foreach ( ParameterMetadata parameterMetadata in methodMetadata.Parameters )
                {
                    if ( parameters.Count( p => p.Name == parameterMetadata.Name ) < 1 )
                    {
                        parameters.Add( parameterMetadata );
                    }
                }
            }

            return parameters;
        }

        private IEnumerable<MethodMetadata> GetMethods( IEnumerable<TypeMetadata> types )
        {
            List<MethodMetadata> methods = new List<MethodMetadata>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                foreach ( MethodMetadata methodMetadata in typeMetadata.Methods )
                {
                    if ( methods.Count( m => m.Name == methodMetadata.Name ) < 1 )
                    {
                        methods.Add( methodMetadata );
                    }
                }
            }

            return methods;
        }

        private IEnumerable<FieldMetadata> GetFields( IEnumerable<TypeMetadata> types )
        {
            List<FieldMetadata> fields = new List<FieldMetadata>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                foreach ( FieldMetadata fieldMetadata in typeMetadata.Fields )
                {
                    if ( fields.Count( f => f.Name == fieldMetadata.Name ) < 1 )
                    {
                        fields.Add( fieldMetadata );
                    }
                }
            }

            return fields;
        }

        private IEnumerable<TypeMetadata> GetTypes( IEnumerable<NamespaceMetadata> namespaces )
        {
            List<TypeMetadata> types = new List<TypeMetadata>();
            foreach ( NamespaceMetadata namespaceMetadata in namespaces )
            {
                foreach ( TypeMetadata typeMetadata in namespaceMetadata.Types )
                {
                    if ( types.Count( t => t.TypeName == typeMetadata.TypeName ) < 1 )
                    {
                        types.Add( typeMetadata );
                    }
                }
            }

            return types;
        }

        private IEnumerable<EventMetadata> GetEvents( IEnumerable<TypeMetadata> types )
        {
            List<EventMetadata> events = new List<EventMetadata>();
            foreach ( TypeMetadata typeMetadata in types )
            {
                foreach ( EventMetadata eventMetadata in typeMetadata.Events )
                {
                    if ( events.Count( e => e.Name == eventMetadata.Name ) < 1 )
                    {
                        events.Add( eventMetadata );
                    }
                }
            }

            return events;
        }

        #endregion
    }
}