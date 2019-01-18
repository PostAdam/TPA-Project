using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using MEFDefinitions;
using Model.ModelDTG;
using ModelBase;

namespace Model
{
    public class Composer
    {
        #region Constructor

        public Composer()
        {
            Compose();
            SetUpDataDirectory();
            string mdfFilePath = ( string ) AppDomain.CurrentDomain.GetData( "DataDirectory" );
            var filename = Path.Combine( mdfFilePath, "DataBase.mdf" );
            if ( !File.Exists( filename ) )
            {
                Task.Run(()=>DropMdfDatabaseFile());
                Task.Run( CreateMdfDatabaseFile ).Wait();
            }

            LoadLogger();
            LoadRepository();
        }

        #endregion

        [ImportMany( typeof( IRepository ) )]
        public IEnumerable<Lazy<IRepository, IDictionary<string, object>>> Repositories;

        [ImportMany( typeof( ITrace ) )]
        public IEnumerable<Lazy<ITrace, IDictionary<string, object>>> Loggers;

        public ITrace Logger;
        public IRepository Repository;

        public void Compose()
        {
            List<DirectoryCatalog> directoryCatalogs = GetDirectoryCatalogs();
            AggregateCatalog catalog = new AggregateCatalog( directoryCatalogs );
            CompositionContainer container = new CompositionContainer( catalog );

            try
            {
                container.ComposeParts( this );
            }
            catch ( CompositionException compositionException )
            {
                Console.WriteLine( compositionException.ToString() );

                throw;
            }
            catch ( Exception exception ) when ( exception is ReflectionTypeLoadException )
            {
                ReflectionTypeLoadException typeLoadException = ( ReflectionTypeLoadException ) exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach( ex => Console.WriteLine( ex.StackTrace ) );
            }
        }

        public async Task<AssemblyMetadata> ReadFromFile( CancellationToken cancellationToken )
        {
            return await Repository.Read( cancellationToken ) is AssemblyMetadataBase assemblyMetadataBase
                ? new AssemblyMetadata( assemblyMetadataBase )
                : null;
        }

        public async Task Save( AssemblyMetadata assemblyMetadata, CancellationToken cancellationTokenSource )
        {
            await Repository.Write( assemblyMetadata.GetOriginalAssemblyMetadata(), cancellationTokenSource );
        }

        #region Private

        private void SetUpDataDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataDirectory = baseDirectory.Remove( baseDirectory.Length - ( "WPF\\bin\\Debug".Length + 1 ) );
            dataDirectory += "DataBase";
            AppDomain.CurrentDomain.SetData( "DataDirectory", dataDirectory );
        }

        private async Task DropMdfDatabaseFile()
        {
            using (SqlConnection connection =
                new SqlConnection(
                    "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True"))
            {
                using (SqlCommand command = new SqlCommand())
                {
                    string stringCommand = "USE MASTER; IF EXISTS ( SELECT 1 FROM master.dbo.sysdatabases WHERE NAME = 'MyDatabase') DROP DATABASE MyDatabase;";

                    command.CommandText = stringCommand;
                    command.Connection = connection;
                    try
                    {
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException sqle)
                    {
                        if (!sqle.Message.Contains("Unable to open the physical file"))
                        {
                            throw;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }

        private async Task CreateMdfDatabaseFile()
        {
            using ( SqlConnection connection =
                new SqlConnection(
                    "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True" ) )
            {
                using ( SqlCommand command = new SqlCommand() )
                {
                    string path = ( string ) AppDomain.CurrentDomain.GetData( "DataDirectory" );
                    string stringCommand = GetStringCommand( path );

                    command.CommandText = stringCommand;
                    command.Connection = connection;
                    try
                    {
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch ( Exception e )
                    {
                        Console.WriteLine( e );
                    }
                }
            }
        }

        private static string GetStringCommand( string path )
        {
            return "CREATE DATABASE MyDatabase ON PRIMARY " +
                   "(NAME = Database_Data, " +
                   $"FILENAME = '{path}\\DataBase.mdf') " +
                   "LOG ON (NAME = MyDatabase_Log, " +
                   $"FILENAME = '{path}\\DataBase.ldf')";
        }

        private List<DirectoryCatalog> GetDirectoryCatalogs()
        {
            NameValueCollection dllsPaths = ConfigurationManager.GetSection( "plugins" ) as NameValueCollection;

            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            if ( dllsPaths != null )
                foreach ( string dllPath in dllsPaths.AllKeys )
                {
                    directoryCatalogs.Add( new DirectoryCatalog( dllPath, "*.dll" ) );
                }

            return directoryCatalogs;
        }

        private void LoadRepository()
        {
            string repositoryType = ConfigurationManager.AppSettings[ "repositoryType" ];
            Repository = Repositories
                .FirstOrDefault( repository => ( string ) repository.Metadata[ "destination" ] == repositoryType )?.Value;
        }

        private void LoadLogger()
        {
            string loggerType = ConfigurationManager.AppSettings[ "loggerType" ];
            Logger = Loggers.FirstOrDefault( logger => ( string ) logger.Metadata[ "destination" ] == loggerType )?.Value;
            if ( Logger != null )
            {
                string logLevel = ConfigurationManager.AppSettings[ "logLevel" ];

                if ( int.TryParse( logLevel, out int level ) )
                {
                    Logger.Level = ( LogLevel ) level;
                }
                else
                {
                    Logger.Level = LogLevel.Warning;
                }
            }
        } 

        #endregion
    }
}