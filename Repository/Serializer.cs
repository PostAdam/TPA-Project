using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using MEFDefinitions;
using Model.Reflection.MetadataModels;

namespace Repository
{
    [Export( typeof( IRepository ) )]
    public class Serializer : IRepository
    {
        private DataContractSerializer _serializer;

        public void Write<T>( T metadata, string fileName )
        {
            //TODO: use DI to inject implementation through method based on config file??
            _serializer = new DataContractSerializer( metadata.GetType() );
            using ( FileStream stream = File.Create( fileName ) )
            {
                //TODO: error proof
                _serializer.WriteObject( stream, metadata );
            }
        }

        public T Read<T>( string filename )
        {
            DataContractSerializer serializer = new DataContractSerializer( typeof( AssemblyMetadata ) );
            T data;
            using ( FileStream stream = File.OpenRead( filename ) )
            {
                data = ( T ) serializer.ReadObject( stream );
            }

            return data;
        }
    }
}