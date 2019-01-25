using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using MEFDefinitions;
using Model;
using Model.ModelDTG;
using Model.Reflection;
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
        private readonly Composer _composer = new Composer();

        public MainViewModel( IPathResolver pathResolver )
        {
            _pathResolver = pathResolver;
            _logger = _composer.Logger;
            Items = new AsyncObservableCollection<MetadataBaseViewModel>();
            ClickSave = new AsyncCommand( Save );
            ClickOpen = new AsyncCommand( Open );
            ClickRead = new AsyncCommand( Read );
            ClickCancelSave = new DelegateCommand( CancelSave );
            ClickCancelRead = new DelegateCommand( CancelRead );
            ClickCloseApp = new DelegateCommand( CloseApp );
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
        public ICommand ClickCloseApp { get; }

        #endregion

        #region Private

        internal AssemblyMetadata AssemblyMetadata;
        private readonly ReflectedTypes _reflectedTypes = ReflectedTypes.Instance;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly ITrace _logger;
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
                _logger?.WriteLine( "Starting serialization process.", LogLevel.Warning.ToString() );

                await _composer.Save( AssemblyMetadata, _cancellationTokenSource.Token );

                _logger?.WriteLine( _isSavingCancelled ? "Serialization cancelled!" : "Serialization completed!",
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
                _logger?.WriteLine( "Opening portable execution file: " + fileName, LogLevel.Debug.ToString() );
                await Task.Run( () => LoadDll( fileName ) ).ContinueWith( _ => InitTreeView( AssemblyMetadata ) );
            }

            IsOpening = false;
        }

        private async Task Read()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            ReadingNotificationText = "Reading in progress ..";
            IsReading = true;
            _logger?.WriteLine( "Reading model.", LogLevel.Information.ToString() );

            AssemblyMetadata = await _composer.ReadFromFile( _cancellationTokenSource.Token );
            if ( AssemblyMetadata != null )
            {
                AddClassesToDirectory( AssemblyMetadata );
                InitTreeView( AssemblyMetadata );
            }

            _logger?.WriteLine( _isReadingCancelled ? "Cancelled reading model!" : "Finished reading model!",
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
                _logger?.WriteLine( "Cancelling saving model, exception message: " + e.Flatten(),
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
                _logger?.WriteLine( "Cancelling reading model, exception message: " + e.Flatten(),
                    LogLevel.Information.ToString() );
            }
        }

        private void CloseApp()
        {
            try
            {
                _logger?.WriteLine( "Attempting to close the application",
                    LogLevel.Information.ToString() );

                _composer.Dispose();

                _logger?.WriteLine( "Application closed successfully",
                    LogLevel.Information.ToString() );
            }
            catch ( Exception e )
            {
                _logger?.WriteLine( "Failed to close the application, exception message: " + e,
                    LogLevel.Information.ToString() );
            }
        }

        #endregion

        #region Help Methods

        internal void AddClassesToDirectory( AssemblyMetadata assemblyMetadata )
        {
            _logger?.WriteLine( "Adding classes to directory.", LogLevel.Information.ToString() );

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

            _logger?.WriteLine( "Classes added to directory!", LogLevel.Information.ToString() );
        }

        internal void InitTreeView( AssemblyMetadata assemblyMetadata )
        {
            _logger?.WriteLine( "Initializing treeView.", LogLevel.Information.ToString() );

            MetadataBaseViewModel metadataViewModel = new AssemblyMetadataViewModel( assemblyMetadata );
            Items.Add( metadataViewModel );

            _logger?.WriteLine( "TreeView initialized!", LogLevel.Information.ToString() );
        }

        internal async Task LoadDll( string path )
        {
            _logger?.WriteLine( "Loading DLL." + path, LogLevel.Trace.ToString() );

            Reflector reflector = new Reflector();
            await reflector.Reflect( path );
            AssemblyMetadata = reflector.AssemblyModel;

            _logger?.WriteLine( "DLL loaded!", LogLevel.Information.ToString() );
        }

        #endregion

        #endregion
    }
}