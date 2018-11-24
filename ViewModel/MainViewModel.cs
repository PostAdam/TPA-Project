﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Input;
using MEFDefinitions;
using Model.Reflection;
using Model.Reflection.MetadataModels;
using ViewModel.Commands;
using ViewModel.Commands.AsyncCommand;
using ViewModel.MetadataViewModels;

namespace ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor

        private readonly IPathResolver _pathResolver;

        public MainViewModel( IPathResolver pathResolver )
        {
            _pathResolver = pathResolver;
            Compose();
            LoadLogger();
            LoadRepository();
            Items = new AsyncObservableCollection<MetadataBaseViewModel>();
            ClickSave = new DelegateCommand( Save );
            ClickOpen = new AsyncCommand( Open );
            ClickRead = new AsyncCommand( Read );
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

        private void LoadRepository()
        {
            string repositoryType = ConfigurationManager.AppSettings["repositoryType"];
            Repository = Repositories
                .FirstOrDefault( repository => (string) repository.Metadata["destination"] == repositoryType )?.Value;
        }

        #endregion

        #region MEF Context

        [ImportMany( typeof( ITrace ) )] public IEnumerable<Lazy<ITrace, IDictionary<string, object>>> Loggers;

        public ITrace Logger;

        [ImportMany( typeof( IRepository ) )]
        public IEnumerable<Lazy<IRepository, IDictionary<string, object>>> Repositories;

        public IRepository Repository;

        private void Compose()
        {
            List<DirectoryCatalog> directoryCatalogs = new List<DirectoryCatalog>()
            {
                new DirectoryCatalog( "../../../XmlRepository/bin/Debug", "*.dll" ),
                new DirectoryCatalog( "../../../DataBaseRepository/bin/Debug", "*.dll" ),
                new DirectoryCatalog( "../../../DataBaseLogger/bin/Debug", "*.dll" ),
                new DirectoryCatalog( "../../../FileLogger/bin/Debug", "*.dll" )
            };
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

                throw;
            }
        }

        #endregion

        public ObservableCollection<MetadataBaseViewModel> Items { get; set; }

        #region Commands

        public ICommand ClickSave { get; }
        public ICommand ClickOpen { get; }
        public ICommand ClickRead { get; }

        #endregion

        #region Private

        internal AssemblyMetadata AssemblyMetadata;
        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;

        #region Command Methods

        private async void Save()
        {
            Logger?.WriteLine( "Starting serializaton process.", LogLevel.Warning.ToString() );
//            string fileName = _pathResolver.SaveFilePath();
//            await Repository.Write<AssemblyMetadata>( AssemblyMetadata, fileName );
            await Repository.Write( AssemblyMetadata, "Test.xml" );
            Logger?.WriteLine( "Serializaton completed!", LogLevel.Error.ToString() );
        }

        private async Task Open()
        {
            string fileName = _pathResolver.OpenFilePath();
            Logger?.WriteLine( "Opening portable execution file: " + fileName, LogLevel.Debug.ToString() );
            await Task.Run( () => LoadDll( fileName ) ).ContinueWith( _ => InitTreeView( AssemblyMetadata ) );
        }

        private async Task Read()
        {
            string fileName = _pathResolver.ReadFilePath();
            await ReadFromFile( fileName );
        }

        #endregion

        #region Help Methods

        private async Task ReadFromFile( string filename )
        {
            Logger?.WriteLine( "Reading from file " + filename + ".", LogLevel.Information.ToString() );

            AssemblyMetadata = ( AssemblyMetadata ) await Repository.Read( filename );
            AddClassesToDirectory( AssemblyMetadata );
            InitTreeView( AssemblyMetadata );

            Logger?.WriteLine( "File " + filename + " deserialized successfully.", LogLevel.Trace.ToString() );
        }

        internal void AddClassesToDirectory( AssemblyMetadata assemblyMetadata )
        {
            Logger?.WriteLine( "Adding classes to directory.", LogLevel.Information.ToString() );
            foreach ( NamespaceMetadata dataNamespace in assemblyMetadata.Namespaces )
            {
                foreach ( TypeMetadata type in dataNamespace.Types )
                {
                    if ( _reflectedTypes.ContainsKey( type.FullName ) == false )
                    {
                        _reflectedTypes.Add( type.FullName, type );
                    }
                }
            }

            Logger?.WriteLine( "Classes added to directory!", LogLevel.Information.ToString() );
        }

        internal void InitTreeView( AssemblyMetadata assemblyMetadata )
        {
            Logger?.WriteLine( "Initializing treeView.", LogLevel.Information.ToString() );
            MetadataBaseViewModel metadataViewModel = new AssemblyMetadataViewModel( assemblyMetadata );
            Items.Add( metadataViewModel );
            Logger?.WriteLine( "TreeView initialized!", LogLevel.Information.ToString() );
        }


        internal async Task LoadDll( string path )
        {
            Logger?.WriteLine( "Loading DLL." + path, LogLevel.Trace.ToString() );
            Reflector reflector = new Reflector();
            await reflector.Reflect( path );
            AssemblyMetadata = reflector.AssemblyModel;
            Logger?.WriteLine( "DLL loaded!", LogLevel.Information.ToString() );
        }

        #endregion

        #endregion
    }
}