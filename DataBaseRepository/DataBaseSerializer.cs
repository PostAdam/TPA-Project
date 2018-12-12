using System.ComponentModel.Composition;
using System.Data.Entity;
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
            await Task.Run( () => WriteData( metadata, fileName ) );
//            WriteData( metadata, fileName );
        }

        public async Task<object> Read( string fileName )
        {
            return await Task.Run( () => ReadData( fileName ) );
//            return ReadData( fileName );
        }

        #region Privates

        private void WriteData( object metadata, string fileName )
        {
            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
            {
                AssemblyMetadataSurrogate assemblyMetadataSurrogate =
                    new AssemblyMetadataSurrogate( metadata as AssemblyMetadata );

                dbContext.AssemblyModels.Add( assemblyMetadataSurrogate );

                dbContext.SaveChanges();
            }
        }

        private object ReadData( string fileName )
        {
            using ( ReflectorDbContext dbContext = new ReflectorDbContext() )
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

                AssemblyMetadataSurrogate assemblyMetadataSurrogate = dbContext.AssemblyModels
                    .Include( a => a.Namespaces ).FirstOrDefault();
//                    .FirstOrDefault( a => a.Name == fileName );

                return assemblyMetadataSurrogate?.GetOriginalAssemblyMetadata();
            }
        }

        #endregion
    }
}