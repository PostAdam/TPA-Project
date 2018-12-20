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

        public async Task<object> Read( string fileName, CancellationToken cancellationToken )
        {
            return await ReadData( fileName, cancellationToken );
        }

        #region Privates

        private AssemblyMetadataSurrogate _assemblyMetadataSurrogate;
        private AssemblyMetadata _assemblyMetadata;

        private async Task WriteData( object metadata, string fileName, CancellationToken cancellationToken )
        {
            ClearAssemblyMetadatas();

            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                try
                {
                    await SavingTasks( metadata, dbContext, cancellationToken );
                }
                catch ( OperationCanceledException e )
                {
                    // TODO: replace with logger
                    Console.WriteLine( e );
                }
                catch ( DbUpdateException e )
                {
                    // TODO: replace with logger
                    Console.WriteLine( e );
                    Console.WriteLine( e.InnerException );
                }
            }
        }

        private async Task SavingTasks( object metadata, ReflectorDbContext dbContext, CancellationToken cancellationToken )
        {
            IEnumerable<Func<CancellationToken, Task>> tasks = PrepareTasksForSaving( dbContext, metadata );
            foreach ( Func<CancellationToken, Task> task in tasks )
            {
                await task( cancellationToken );
                cancellationToken.ThrowIfCancellationRequested();
            }
        }

        private async Task<object> ReadData( string fileName, CancellationToken cancellationToken )
        {
            ClearAssemblyMetadatas();

            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                try
                {
                    await ReadingTasks( dbContext, cancellationToken );
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

            return _assemblyMetadata;
        }

        private async Task ReadingTasks( ReflectorDbContext dbContext, CancellationToken cancellationToken )
        {
            IEnumerable<Func<CancellationToken, Task>> tasks =
                                    PrepareTasksForReading( dbContext );
            foreach ( Func<CancellationToken, Task> task in tasks )
            {
                await task( cancellationToken );
                cancellationToken.ThrowIfCancellationRequested();
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

        private IEnumerable<Func<CancellationToken, Task>> PrepareTasksForReading( ReflectorDbContext dbContext )
        {
            return new List<Func<CancellationToken, Task>>()
            {
                ct => Task.Run( () => IncludeRelatedModelsObjects( dbContext ), ct ),
                async ct => _assemblyMetadataSurrogate = await dbContext.AssemblyModels
                    .Include( a => a.Namespaces ).FirstOrDefaultAsync( ct ),
                ct => Task.Run(
                    () => _assemblyMetadata = _assemblyMetadataSurrogate?.GetOriginalAssemblyMetadata(), ct )
            };
        }

        private IEnumerable<Func<CancellationToken, Task>> PrepareTasksForSaving( ReflectorDbContext dbContext,
            object metadata )
        {
            return new List<Func<CancellationToken, Task>>()
            {
                ct => Task.Run(
                    () => _assemblyMetadataSurrogate =
                        new AssemblyMetadataSurrogate( metadata as AssemblyMetadata ), ct ),
                ct => Task.Run( () => dbContext.AssemblyModels.Add( _assemblyMetadataSurrogate ), ct ),
                dbContext.SaveChangesAsync
            };
        }

        private void ClearAssemblyMetadatas()
        {
            _assemblyMetadata = null;
            _assemblyMetadataSurrogate = null;
        }

        #endregion
    }
}