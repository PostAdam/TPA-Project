using System;
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
    public class Composer : IDisposable
    {
        #region Constructor

        public Composer()
        {
            Compose();
            SetUpDataDirectory();
            LoadLogger();
            LoadRepository();
        }

        #endregion

        [ImportMany(typeof(IRepository))]
        public IEnumerable<Lazy<IRepository, IDictionary<string, object>>> Repositories;

        [ImportMany(typeof(ITrace))]
        public IEnumerable<Lazy<ITrace, IDictionary<string, object>>> Loggers;

        public ITrace Logger;
        public IRepository Repository;

        public void Compose()
        {
            List<DirectoryCatalog> directoryCatalogs = GetDirectoryCatalogs();
            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            _container = new CompositionContainer(catalog);

            try
            {
                _container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());

                throw;
            }
            catch ( ReflectionTypeLoadException exception )
            {
                ReflectionTypeLoadException typeLoadException = exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach(ex => Console.WriteLine(ex.StackTrace));
            }
        }

        public async Task<AssemblyMetadata> ReadFromFile(CancellationToken cancellationToken)
        {
            return await Repository.Read(cancellationToken) is AssemblyMetadataBase assemblyMetadataBase
                ? new AssemblyMetadata(assemblyMetadataBase)
                : null;
        }

        public async Task Save(AssemblyMetadata assemblyMetadata, CancellationToken cancellationTokenSource)
        {
            await Repository.Write(assemblyMetadata.GetOriginalAssemblyMetadata(), cancellationTokenSource);
        }

        public void Dispose()
        {
            _container.Dispose();
        }

        #region Private

        private CompositionContainer _container;

        private void SetUpDataDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string dataDirectory = baseDirectory.Remove(baseDirectory.Length - ("WPF\\bin\\Debug".Length + 1));
            dataDirectory += "DataBase";
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);
        }

        private List<DirectoryCatalog> GetDirectoryCatalogs()
        {
            NameValueCollection dllsPaths = ConfigurationManager.GetSection("plugins") as NameValueCollection;

            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>();
            if (dllsPaths != null)
                foreach (string dllPath in dllsPaths.AllKeys)
                {
                    directoryCatalogs.Add(new DirectoryCatalog(dllPath, "*.dll"));
                }

            return directoryCatalogs;
        }

        private void LoadRepository()
        {
            string repositoryType = ConfigurationManager.AppSettings[ "repositoryType" ];
            Repository = Repositories
                ?.FirstOrDefault(repository => ( string ) repository.Metadata[ "destination" ] == repositoryType)?.Value;
        }

        private void LoadLogger()
        {
            string loggerType = ConfigurationManager.AppSettings[ "loggerType" ];
            Logger = Loggers?.FirstOrDefault(logger => ( string ) logger.Metadata[ "destination" ] == loggerType)?.Value;
            if (Logger != null)
            {
                Logger.Level = GetLogLevel();
            }
        }

        private LogLevel GetLogLevel()
        {
            string logLevel = ConfigurationManager.AppSettings[ "logLevel" ];

            if (int.TryParse(logLevel, out int level))
            {
                return ( LogLevel ) level;
            }
            else
            {
                return LogLevel.Warning;
            }
        }

        #endregion
    }
}