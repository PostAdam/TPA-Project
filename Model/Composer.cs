using System;
using System.Collections.Generic;
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
        [ImportMany(typeof(IRepository))]
        public IEnumerable<Lazy<IRepository, IDictionary<string, object>>> Repositories;

        public IRepository Repository;

        public Composer()
        {
            Compose();
            LoadRepository();
        }

        public void Compose()
        {
            // TODO: move to App.config
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>()
            {
                new DirectoryCatalog("../../../XmlRepository/bin/Debug", "*.dll"),
                new DirectoryCatalog("../../../DataBaseRepository/bin/Debug", "*.dll"),
                new DirectoryCatalog("../../../DataBaseLogger/bin/Debug", "*.dll"),
                new DirectoryCatalog("../../../FileLogger/bin/Debug", "*.dll")
            };
            AggregateCatalog catalog = new AggregateCatalog(directoryCatalogs);
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());

                throw;
            }
            catch (Exception exception) when (exception is ReflectionTypeLoadException)
            {
                ReflectionTypeLoadException typeLoadException = (ReflectionTypeLoadException) exception;
                Exception[] loaderExceptions = typeLoadException.LoaderExceptions;
                loaderExceptions.ToList().ForEach(ex => Console.WriteLine(ex.StackTrace));

                throw;
            }
        }

        private void LoadRepository()
        {
            string repositoryType = ConfigurationManager.AppSettings["repositoryType"];
            Repository = Repositories
                .FirstOrDefault(repository => (string) repository.Metadata["destination"] == repositoryType)?.Value;
        }

        public async Task<AssemblyMetadata> ReadFromFile( string filename, CancellationTokenSource cancellationToken )
        {
            AssemblyMetadataBase assemblyMetadataBase = await Repository.Read( filename, cancellationToken.Token ) as AssemblyMetadataBase;

            return new AssemblyMetadata( assemblyMetadataBase );
        }

        public async Task Save( AssemblyMetadata assemblyMetadata , string filename, CancellationToken _cancellationTokenSource )
        {
            await Repository.Write( assemblyMetadata.GetOriginalAssemblyMetadata(), filename, _cancellationTokenSource );
        }
    }
}