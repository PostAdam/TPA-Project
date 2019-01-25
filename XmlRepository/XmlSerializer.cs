using System;
using System.ComponentModel.Composition;
using System.Configuration;
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
        public async Task Write( AssemblyMetadataBase metadata, CancellationToken cancellationToken )
        {
            await Task.Run( () => TryWriteData( metadata, cancellationToken ), cancellationToken );
        }

        public async Task<AssemblyMetadataBase> Read( CancellationToken cancellationToken )
        {
            return await Task.Run( () => TryReadData( cancellationToken ), cancellationToken );
        }

        #region Privates

        private AssemblyMetadataSurrogate _assemblyMetadataSurrogate;

        private readonly DataContractSerializer _serializer =
            new DataContractSerializer( typeof( AssemblyMetadataSurrogate ) );

        private readonly string _fileName = ConfigurationManager.AppSettings[ "repositoryFileName" ];

        private async Task TryWriteData( AssemblyMetadataBase metadata, CancellationToken cancellationToken )
        {
            try
            {
                await Task.Run( () => WriteData( metadata, cancellationToken ), cancellationToken );
            }
            catch ( OperationCanceledException e )
            {
                // TODO: replace with logger
                Console.WriteLine( e );
            }
            catch ( Exception e )
            {
                // TODO: replace with logger
                Console.WriteLine( e );
            }
        }

        private void WriteData( AssemblyMetadataBase metadata, CancellationToken cancellationToken )
        {
            _assemblyMetadataSurrogate = new AssemblyMetadataSurrogate( metadata );

            cancellationToken.ThrowIfCancellationRequested();
            using ( FileStream stream = File.Create( _fileName ) )
            {
                _serializer.WriteObject( stream, _assemblyMetadataSurrogate );
            }

            cancellationToken.ThrowIfCancellationRequested();
        }

        private AssemblyMetadataBase TryReadData( CancellationToken cancellationToken )
        {
            AssemblyMetadataBase assemblyMetadataBase = null;
            try
            {
                assemblyMetadataBase =
                    Task.Run( () => ReadData( cancellationToken ), cancellationToken ).Result;
            }
            catch ( OperationCanceledException e )
            {
                // TODO: replace with logger
                Console.WriteLine( e );
            }
            catch ( Exception e )
            {
                // TODO: replace with logger
                Console.WriteLine( e );
            }

            return assemblyMetadataBase;
        }

        private AssemblyMetadataBase ReadData( CancellationToken cancellationToken )
        {
            cancellationToken.ThrowIfCancellationRequested();

            using ( FileStream stream = File.OpenRead( _fileName ) )
            {
                _assemblyMetadataSurrogate = (AssemblyMetadataSurrogate) _serializer.ReadObject( stream );
            }

            cancellationToken.ThrowIfCancellationRequested();

            AssemblyMetadataBase assemblyMetadata = _assemblyMetadataSurrogate.GetOriginalAssemblyMetadata();

            cancellationToken.ThrowIfCancellationRequested();

            return assemblyMetadata;
        }

        #endregion
    }
}