using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using DataBaseSerializationSurrogates.MetadataSurrogates;
using MEFDefinitions;
using Model.Reflection.MetadataModels;

namespace DataBaseRepository
{
    [Export( typeof( IRepository ) )]
    [ExportMetadata( "destination", "db" )]
    public class DataBaseSerializer : IRepository
    {
        public async Task Write( object metadata, string fileName )
        {
//            await Task.Run( () => WriteData( metadata, fileName ) );
            WriteData( metadata, fileName );
        }

        public async Task<object> Read( string fileName )
        {
            return await Task.Run( () => ReadData( fileName ) );
        }

        #region Privates

        private void WriteData( object metadata, string fileName )
        {
            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                AssemblyMetadataSurrogate assemblyMetadataSurrogate =
                    new AssemblyMetadataSurrogate( (AssemblyMetadata) metadata );
                dbContext.Assemblies.Add( assemblyMetadataSurrogate );
                dbContext.Namespaces.AddRange( assemblyMetadataSurrogate.Namespaces );
                //                dbContext.Types.AddRange( metadata.Namespaces.Select( n => n.Types ) );

                dbContext.SaveChanges();

                IQueryable<AssemblyMetadataSurrogate> query = from b in dbContext.Assemblies
                    select b;
                Console.WriteLine( query );
            }
        }

        private object ReadData( string fileName )
        {
            return null;
        }

        #endregion
    }
}