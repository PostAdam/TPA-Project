using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MEFDefinitions;
using Microsoft.Extensions.Logging;
using Model.Reflection;
using Model.Reflection.MetadataModels;
using ViewModel.Commands;
using ViewModel.MetadataViewModels;

namespace ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor

        private readonly IPathResolver _pathResolver;

        private delegate string GetFilePath();
        public MainViewModel(IPathResolver pathResolver)
        {
            _pathResolver = pathResolver;
            Compose();
            LoadLogger();
            Items = new AsyncObservableCollection<MetadataBaseViewModel>();
            ClickSave = new DelegateCommand(Save);
            ClickOpen = new DelegateCommand(Open);
            ClickRead = new DelegateCommand(Read);
        }

        private void LoadLogger()
        {
            string loggerType = ConfigurationManager.AppSettings["loggerType"];
            Logger = Loggers.FirstOrDefault(logger => (string) logger.Metadata["destination"] == loggerType)?.Value;
            if ( Logger != null )
            {
                string logLevel = ConfigurationManager.AppSettings["logLevel"];

                if ( int.TryParse(logLevel, out int level) )
                {
                    Logger.Level = (LogLevel) level;
                }
                else
                {
                    Logger.Level = LogLevel.Warning;
                }
            }
        }

        #endregion

        #region DataContext

        [ImportMany(typeof(ITrace))] public IEnumerable<Lazy<ITrace, IDictionary<string, object>>> Loggers;

        public ITrace Logger;

        [Import(typeof(ISerializer))] public ISerializer Serializer;

        public ObservableCollection<MetadataBaseViewModel> Items { get; set; }

        #endregion

        #region Private

        private void Compose()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../Model/bin/Debug", "*.dll"));
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch ( CompositionException compositionException )
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        public ICommand ClickSave { get; }
        public ICommand ClickOpen { get; }
        public ICommand ClickRead { get; }
        internal AssemblyMetadata AssemblyMetadata;

        #region Methods

        public void Save()
        {
            Logger?.WriteLine("Starting serializaton process.", LogLevel.Warning.ToString());
            string fileName = _pathResolver.SaveFilePath();
            Serializer.Serialize<AssemblyMetadata>(AssemblyMetadata, fileName);
            Logger?.WriteLine("Serializaton completed!", LogLevel.Error.ToString());
        }

        public async void Open()
        {
            string fileName = _pathResolver.OpenFilePath();
            Logger?.WriteLine("Opening portable execution file: " + fileName, LogLevel.Debug.ToString());
            await Task.Run(() => LoadDll(fileName)).ContinueWith(_ => InitTreeView(AssemblyMetadata));
        }

        public void Read()
        {
            string fileName = _pathResolver.OpenFilePath();
            ReadFromFile(fileName);
        }

        #endregion

        #region Help Methods

        private void ReadFromFile(string filename)
        {
            Logger?.WriteLine("Reading from file " + filename + ".", LogLevel.Information.ToString());
            AssemblyMetadata data = Serializer.Deserialize<AssemblyMetadata>(filename);
            AddClassesToDirectory(data);
            InitTreeView(data);
            Logger?.WriteLine("File " + filename + " deserialized successfully.", LogLevel.Trace.ToString());
        }

        internal void AddClassesToDirectory(AssemblyMetadata assemblyMetadata)
        {
            Logger?.WriteLine("Adding classes to directory.", LogLevel.Information.ToString());
            foreach ( NamespaceMetadata dataNamespace in assemblyMetadata.Namespaces )
            {
                foreach ( TypeMetadata type in dataNamespace.Types )
                {
                    if ( TypesDictionary.ReflectedTypes.ContainsKey(type.TypeName) == false )
                    {
                        TypesDictionary.ReflectedTypes.Add(type.TypeName, type);
                    }
                }
            }

            Logger?.WriteLine("Classes added to directory!", LogLevel.Information.ToString());
        }

        internal void InitTreeView(AssemblyMetadata assemblyMetadata)
        {
            Logger?.WriteLine("Initializing treeView.", LogLevel.Information.ToString());
            MetadataBaseViewModel metadataViewModel = new AssemblyMetadataViewModel(assemblyMetadata);
            Items.Add(metadataViewModel);
            Logger?.WriteLine("TreeView initialized!", LogLevel.Information.ToString());
        }


        internal void LoadDll(string path)
        {
            Logger?.WriteLine("Loading DLL." + path, LogLevel.Trace.ToString());
            Reflector reflector = new Reflector();
            reflector.Reflect(path);
            AssemblyMetadata = reflector.AssemblyModel;
            Logger?.WriteLine("DLL loaded!", LogLevel.Information.ToString());
        }

        #endregion

        #endregion
    }
}