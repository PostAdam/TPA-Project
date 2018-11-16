using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MEFDefinitions;
using Model.Reflection.MetadataModels;

namespace XmlRepository
{
    [Export( typeof( IRepository ) )]
    [ExportMetadata( "destination", "file" )]
    public class XmlSerializer : IRepository
    {
        private DataContractSerializer _serializer;

        public async Task Write<T>( T metadata, string fileName ) where T : class
        {
            await Task.Run( () => WriteData( metadata, fileName ) );
        }

        public async Task<T> Read<T>( string fileName ) where T : class
        {
            return await Task.Run( () => ReadData<T>( fileName ) );
        }

        public void WriteData<T>( T metadata, string fileName )
        {
            //TODO: use DI to inject implementation through method based on config file??
            _serializer = new DataContractSerializer( metadata.GetType() );
            using ( FileStream stream = File.Create( fileName ) )
            {
                //TODO: error proof
                _serializer.WriteObject( stream, metadata );
            }
        }

        private T ReadData<T>( string fileName )
        {
            DataContractSerializer serializer = new DataContractSerializer( typeof( AssemblyMetadata ) );
            T data;
            using ( FileStream stream = File.OpenRead( fileName ) )
            {
                data = ( T ) serializer.ReadObject( stream );
            }

            return data;
        }
    }
}