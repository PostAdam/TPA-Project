using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading;
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
        public async Task Write( object metadata, string fileName, CancellationToken cancellationToken )
        {
            await Task.Run( () => WriteData( metadata, fileName, cancellationToken ), cancellationToken );
        }

        public async Task<object> Read( string fileName )
        {
            return await ReadData( fileName );
        }

        #region Privates

        private async Task WriteData( object metadata, string fileName, CancellationToken cancellationToken )
        {
            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                try
                {
                    AssemblyMetadataSurrogate assemblyMetadataSurrogate = null;

                    IEnumerable<Func<CancellationToken, Task>> tasks = new List<Func<CancellationToken, Task>>()
                    {
                        ct => Task.Run(
                            () => assemblyMetadataSurrogate =
                                new AssemblyMetadataSurrogate( metadata as AssemblyMetadata ), ct ),
                        ct => Task.Run( () => dbContext.AssemblyModels.Add( assemblyMetadataSurrogate ), ct ),
                        ct => dbContext.SaveChangesAsync( ct )
                    };

                    foreach ( Func<CancellationToken, Task> task in tasks )
                    {
                        await task( cancellationToken );
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                }
                catch ( OperationCanceledException e )
                {
                    Console.WriteLine( e );
                }
                catch ( DbUpdateException e )
                {
                    Console.WriteLine( e );
                    Console.WriteLine( e.InnerException );
                }
            }
        }

        private async Task<object> ReadData( string fileName )
        {
            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                await Task.Run( () => IncludeRelatedModelsObjects( dbContext ) );
                AssemblyMetadataSurrogate assemblyMetadataSurrogate =
                    await dbContext.AssemblyModels.Include( a => a.Namespaces ).FirstOrDefaultAsync();

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