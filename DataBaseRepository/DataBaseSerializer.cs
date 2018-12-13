using System.ComponentModel.Composition;
using System.Data.Entity;
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
        // TODO: remove fileName argument and change way of choosing it
        public async Task Write( object metadata, string fileName )
        {
            await WriteData( metadata, fileName );
        }

        public async Task<object> Read( string fileName )
        {
            return await ReadData( fileName );
        }

        #region Privates

        private async Task WriteData( object metadata, string fileName )
        {
            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                AssemblyMetadataSurrogate assemblyMetadataSurrogate =
                    await Task.Run( () => new AssemblyMetadataSurrogate( metadata as AssemblyMetadata ) );
                await Task.Run( () => dbContext.AssemblyModels.Add( assemblyMetadataSurrogate ) );
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task<object> ReadData( string fileName )
        {
            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                await Task.Run( () => IncludeRelatedModelsObjects( dbContext ) );
                AssemblyMetadataSurrogate assemblyMetadataSurrogate = await dbContext.AssemblyModels
                    .Include( a => a.Namespaces ).FirstOrDefaultAsync();

                return await Task.Run( () => assemblyMetadataSurrogate?.GetOriginalAssemblyMetadata() );
            }
        }

        private void IncludeRelatedModelsObjects( ReflectorDbContext dbContext )
        {
            dbContext.NamespaceModels
                .Include( n => n.Types )
                .Load();

            dbContext.TypeModels
                .Include( t => t.BaseType )
                .Include( t => t.DeclaringType )
                .Include( t => t.Fields )
                .Include( t => t.GenericArguments )
                .Include( t => t.Attributes )
                .Include( t => t.ImplementedInterfaces )
                .Include( t => t.NestedTypes )
                .Include( t => t.Properties )
                .Include( t => t.Methods )
                .Include( t => t.Constructors )
                .Include( t => t.EventSurrogates )
                .Load();

            dbContext.ParameterModels
                .Include( p => p.TypeMetadata )
                .Include( p => p.ParameterAttributes )
                .Load();

            dbContext.PropertiesModels
                .Include( p => p.PropertyAttributes )
                .Include( p => p.TypeMetadata )
                .Include( p => p.Getter )
                .Include( p => p.Setter )
                .Load();

            dbContext.MethodModels
                .Include( m => m.ReturnType )
                .Include( m => m.MethodAttributes )
                .Include( m => m.Parameters )
                .Include( m => m.GenericArguments )
                .Load();

            dbContext.FieldModels
                .Include( f => f.TypeMetadata )
                .Include( f => f.FieldAttributes )
                .Load();

            dbContext.EventModels
                .Include( e => e.TypeMetadata )
                .Include( e => e.AddMethodMetadata )
                .Include( e => e.RaiseMethodMetadata )
                .Include( e => e.RemoveMethodMetadata )
                .Include( e => e.EventAttributes )
                .Load();
        }

        #endregion
    }
}