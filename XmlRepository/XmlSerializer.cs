using System.ComponentModel.Composition;
using System.IO;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using MEFDefinitions;
using ModelBase;
using XmlSerializationSurrogates.MetadataSurrogates;

namespace XmlRepository
{
    [Export( typeof( IRepository ) )]
    [ExportMetadata( "destination", "file" )]
    public class XmlSerializer : IRepository
    {
        public async Task Write( object metadata, string fileName, CancellationToken cancellationToken )
        {
            await Task.Run( () => WriteData( metadata, fileName ), cancellationToken );
        }

        public async Task<object> Read( string fileName, CancellationToken cancellationToken )
        {
            return await Task.Run( () => ReadData( fileName ), cancellationToken );
        }

        #region Privates

        private void WriteData( object metadata, string fileName )
        {
            //TODO: use DI to inject implementation through method based on config file??
            AssemblyMetadataSurrogate assemblyMetadataSurrogate =
                new AssemblyMetadataSurrogate( (AssemblyMetadataBase) metadata );
            DataContractSerializer serializer = new DataContractSerializer( typeof( AssemblyMetadataSurrogate ) );

            using ( FileStream stream = File.Create( fileName ) )
            {
                //TODO: error proof
                serializer.WriteObject( stream, assemblyMetadataSurrogate );
            }
        }

        private object ReadData( string fileName )
        {
            DataContractSerializer serializer = new DataContractSerializer( typeof( AssemblyMetadataSurrogate ) );
            AssemblyMetadataSurrogate assemblyMetadataSurrogate;

            using ( FileStream stream = File.OpenRead( fileName ) )
            {
                assemblyMetadataSurrogate = (AssemblyMetadataSurrogate) serializer.ReadObject( stream );
            }

            AssemblyMetadataBase assemblyMetadata = assemblyMetadataSurrogate.GetOriginalAssemblyMetadata();

            return assemblyMetadata;
        }

        #endregion
    }
}