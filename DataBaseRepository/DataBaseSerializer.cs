using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading.Tasks;
using MEFDefinitions;

namespace DataBaseRepository
{
    [Export( typeof( IRepository ) )]
    [ExportMetadata( "destination", "db" )]
    public class DataBaseSerializer : IRepository
    {
        private readonly string _connectionString;

        //public DataBaseSerializer()
        //{
        //    _connectionString = GetConnectionString();
        //}

        public async Task Write<T>( T metadata, string fileName ) where T : class
        {
            await Task.Run( () => WriteData( metadata, fileName ) );
        }

        public async Task<T> Read<T>( string fileName ) where T : class
        {
            return await Task.Run( () => ReadData<T>( fileName ) );
        }

        public void WriteData<T>( T metadata, string fileName ) where T : class
        {
            using ( ReflectorDbContext<T> dbContext = new ReflectorDbContext<T>() )
            {
                dbContext.Assemblies.Add( metadata );
                dbContext.SaveChanges();
                var query = from b in dbContext.Assemblies
                    select b;
                Console.WriteLine( query );
            }
        }

        private T ReadData<T>( string fileName )
        {
            return (T) ( new object() );
        }

        /*private string GetConnectionString()
        {
            Configuration appConfig =
                ConfigurationManager.OpenExeConfiguration( Assembly.GetExecutingAssembly().Location );
            string connectionString = appConfig.ConnectionStrings
                .ConnectionStrings[ /*"Model.Properties.Settings.LoggingDataBaseConnectionString"#1#""].ConnectionString;
            return connectionString;
        }*/

    }
}