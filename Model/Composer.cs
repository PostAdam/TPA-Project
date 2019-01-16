﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
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
        [ImportMany( typeof( IRepository ) )]
        public IEnumerable<Lazy<IRepository, IDictionary<string, object>>> Repositories;

        [ImportMany( typeof( ITrace ) )] public IEnumerable<Lazy<ITrace, IDictionary<string, object>>> Loggers;

        public ITrace Logger;
        public IRepository Repository;

        public Composer()
        {
            Compose();
            LoadRepository();
            LoadLogger();
            SetUpDataDirectory();
        }

        private void SetUpDataDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataDirectory = baseDirectory.Remove( baseDirectory.Length - ( "WPF\\bin\\Debug".Length + 1 ) );
            dataDirectory += "DataBase";
            AppDomain.CurrentDomain.SetData( "DataDirectory", dataDirectory );
        }

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
                ReflectionTypeLoadException typeLoadException = (ReflectionTypeLoadException) exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach( ex => Console.WriteLine( ex.StackTrace ) );
            }
        }

        private List<DirectoryCatalog> GetDirectoryCatalogs()
        {
            NameValueCollection dllsPaths = ConfigurationManager.GetSection( "plugins" ) as NameValueCollection;

            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            foreach ( string dllPath in dllsPaths.AllKeys )
            {
                directoryCatalogs.Add( new DirectoryCatalog( dllPath, "*.dll" ) );
            }

            return directoryCatalogs;
        }

        private void LoadRepository()
        {
            string repositoryType = ConfigurationManager.AppSettings["repositoryType"];
            Repository = Repositories
                .FirstOrDefault( repository => (string) repository.Metadata["destination"] == repositoryType )?.Value;
        }

        private void LoadLogger()
        {
            string loggerType = ConfigurationManager.AppSettings["loggerType"];
            Logger = Loggers.FirstOrDefault( logger => (string) logger.Metadata["destination"] == loggerType )?.Value;
            if ( Logger != null )
            {
                string logLevel = ConfigurationManager.AppSettings["logLevel"];

                if ( int.TryParse( logLevel, out int level ) )
                {
                    Logger.Level = (LogLevel) level;
                }
                else
                {
                    Logger.Level = LogLevel.Warning;
                }
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
    }
}