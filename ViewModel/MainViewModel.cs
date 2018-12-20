using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MEFDefinitions;
using Model.Reflection;
using Model.Reflection.MetadataModels;
using PropertyChanged;
using ViewModel.Commands;
using ViewModel.Commands.NewAsyncCommand;
using ViewModel.MetadataViewModels;

namespace ViewModel
{
    [AddINotifyPropertyChangedInterface]
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
            ClickSave = new AsyncCommand( Save );
            ClickOpen = new AsyncCommand( Open );
            ClickRead = new AsyncCommand( Read );
            ClickCancelSave = new DelegateCommand( CancelSave );
            ClickCancelRead = new DelegateCommand( CancelRead );
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
            // TODO: move to App.config
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

        public bool IsOpening { get; set; }
        public bool IsSaving { get; set; }
        public bool IsReading { get; set; }

        public string SavingNotificationText { get; set; }
        public string ReadingNotificationText { get; set; }

        #region Commands

        public AsyncCommand ClickSave { get; }
        public AsyncCommand ClickOpen { get; }
        public AsyncCommand ClickRead { get; }
        public ICommand ClickCancelSave { get; }
        public ICommand ClickCancelRead { get; }

        #endregion

        #region Private

        internal AssemblyMetadata AssemblyMetadata;
        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;
        private CancellationTokenSource _cancellationTokenSource;

        private bool _isSavingCancelled;
        private bool _isReadingCancelled;

        #region Command Methods

        private async Task Save()
        {
            if ( AssemblyMetadata != null )
            {
                _cancellationTokenSource = new CancellationTokenSource();
                SavingNotificationText = "Saving in progress ..";
                IsSaving = true;
                Logger?.WriteLine( "Starting serializaton process.", LogLevel.Warning.ToString() );

                
                // TODO: find solution to pass filepath
                // string fileName = _pathResolver.SaveFilePath();
                await Repository.Write( AssemblyMetadata, "Test.xml", _cancellationTokenSource.Token );

                Logger?.WriteLine( _isSavingCancelled ? "Serializaton cancelled!" : "Serializaton completed!",
                    LogLevel.Information.ToString() );

                IsSaving = false;
                _isSavingCancelled = false;
            }
        }

        private async Task Open()
        {
            IsOpening = true;

            string fileName = _pathResolver.OpenFilePath();
            if ( fileName != null )
            {
                Logger?.WriteLine( "Opening portable execution file: " + fileName, LogLevel.Debug.ToString() );
                await Task.Run( () => LoadDll( fileName ) ).ContinueWith( _ => InitTreeView( AssemblyMetadata ) );
            }

            IsOpening = false;
        }

        private async Task Read()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            ReadingNotificationText = "Reading in progress ..";
            IsReading = true;
            Logger?.WriteLine( "Reading model.", LogLevel.Information.ToString() );

            // TODO: find solution to pass filepath
            // string fileName = _pathResolver.ReadFilePath();
            await ReadFromFile( "ViewModel.xml", _cancellationTokenSource );

            Logger?.WriteLine( _isReadingCancelled ? "Cancelled reading model!" : "Finished reading model!",
                LogLevel.Information.ToString() );

            IsReading = false;
            _isReadingCancelled = false;
        }

        private void CancelSave()
        {
            if ( _isSavingCancelled || _cancellationTokenSource == null ) return;

            try
            {
                _isSavingCancelled = true;
                SavingNotificationText = "Cancelling saving ..";
                _cancellationTokenSource.Cancel();
            }
            catch ( AggregateException e )
            {
                Logger?.WriteLine( "Cancelling saving model, exception message: " + e.Flatten(),
                    LogLevel.Information.ToString() );
            }
        }

        private void CancelRead()
        {
            if ( _isReadingCancelled || _cancellationTokenSource == null ) return;

            try
            {
                _isReadingCancelled = true;
                ReadingNotificationText = "Cancelling reading ..";
                _cancellationTokenSource.Cancel();
            }
            catch ( AggregateException e )
            {
                Logger?.WriteLine( "Cancelling reading model, exception message: " + e.Flatten(),
                    LogLevel.Information.ToString() );
            }
        }

        #endregion

        #region Help Methods

        private async Task ReadFromFile( string filename, CancellationTokenSource cancellationToken )
        {
            AssemblyMetadata = await Repository.Read( filename, cancellationToken.Token ) as AssemblyMetadata;
            if ( AssemblyMetadata != null )
            {
                AddClassesToDirectory( AssemblyMetadata );
                InitTreeView( AssemblyMetadata );
            }
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