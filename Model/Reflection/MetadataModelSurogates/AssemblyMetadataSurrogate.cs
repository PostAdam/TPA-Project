/*using System.Collections.Generic;
using System.Runtime.Serialization;
using Model.Reflection.MetadataModels;

namespace Model.Reflection.MetadataModelSurogates
{
    public class AssemblyMetadataSurrogate : ISerialization
    {
        public void GetObjectData( object obj, SerializationInfo info, StreamingContext context )
        {
            AssemblyMetadata assemblyMetadata = (AssemblyMetadata) obj;
            info.AddValue( "Name", assemblyMetadata.Name );
            info.AddValue( "Namespaces", assemblyMetadata.Namespaces );
        }

        public object SetObjectData( object obj, SerializationInfo info, StreamingContext context,
            ISelector selector )
        {
            AssemblyMetadata assemblyMetadata = (AssemblyMetadata) obj;
            assemblyMetadata.Name = info.GetString( "Name" );
            assemblyMetadata.Namespaces =
                (IEnumerable<NamespaceMetadata>) info.GetValue( "Namespaces",
                    typeof( IEnumerable<NamespaceMetadata> ) );
            return assemblyMetadata;
        }
    }
}*/