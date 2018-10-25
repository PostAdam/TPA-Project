using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Linq;
using System.Windows.Input;
using MEFDefinitions;
using Microsoft.Extensions.Logging;
using Model.Reflection;
using Model.Reflection.MetadataModels;
using ViewModel.Commands;
using ViewModel.MetadataBaseViewModels;
using ViewModel.MetadataViewModels;

namespace ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        #region Constructor

        public MainViewModel()
        {
            Compose();
            LoadLogger();
            Items = new ObservableCollection<MetadataBaseViewModel>();
            ClickSave = new DelegateCommand(Save);
        }

        private void LoadLogger()
        {
            string loggerType = ConfigurationManager.AppSettings["loggerType"];
            Logger = Loggers.FirstOrDefault(logger => (string) logger.Metadata["destination"] == loggerType)?.Value;
            if (Logger != null)
            {
                string logLevel = ConfigurationManager.AppSettings["logLevel"];

                if (int.TryParse(logLevel, out var level))
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
            var catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new DirectoryCatalog("../../../Model/bin/Debug", "*.dll"));
            CompositionContainer container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        public ICommand ClickSave { get; }
        internal AssemblyMetadata AssemblyMetadata;

        #region Methods

        public void Save()
        {
            Logger?.Log("Starting serializaton process.", LogLevel.Warning);
            Serializer.Serialize<AssemblyMetadata>(AssemblyMetadata);
            Logger?.Log("Serializaton completed!", LogLevel.Error);
        }

        public void Open(string fileName)
        {
            Logger?.Log("Opening portable execution file: " + fileName, LogLevel.Debug);

            LoadDll(fileName);
            InitTreeView(AssemblyMetadata);
        }

        public void Read(string fileName)
        {
            ReadFromFile(fileName);
        }

        #endregion

        #region Help Methods

        public void ReadFromFile(string filename)
        {
            Logger?.Log("Reading from file " + filename + ".", LogLevel.Information);
            AssemblyMetadata data = Serializer.Deserialize<AssemblyMetadata>(filename);
            AddClassesToDirectory(data);
            InitTreeView(data);
            Logger?.Log("File " + filename + " deserialized successfully.", LogLevel.Trace);
        }

        internal void AddClassesToDirectory(AssemblyMetadata assemblyMetadata)
        {
            Logger?.Log("Adding classes to directory.", LogLevel.Information);
            foreach (NamespaceMetadata dataNamespace in assemblyMetadata.Namespaces)
            {
                foreach (TypeMetadata type in dataNamespace.Types)
                {
                    if (TypesDictionary.ReflectedTypes.ContainsKey(type.TypeName) == false)
                    {
                        TypesDictionary.ReflectedTypes.Add(type.TypeName, type);
                    }
                }
            }

            Logger?.Log("Classes added to directory!", LogLevel.Information);
        }

        internal void InitTreeView(AssemblyMetadata assemblyMetadata)
        {
            Logger?.Log("Initializing treeView.", LogLevel.Information);
            MetadataBaseViewModel metadataViewModel = new AssemblyMetadataViewModel(assemblyMetadata);
            Items.Add(metadataViewModel);
            Logger?.Log("TreeView initialized!", LogLevel.Information);
        }

        public void Init(string filename)
        {
            LoadDll(filename);
            InitTreeView(AssemblyMetadata);
        }

        internal void LoadDll(string path)
        {
            Logger?.Log("Loading DLL." + path, LogLevel.Trace);
            Reflector reflector = new Reflector();
            reflector.Reflect(path);
            AssemblyMetadata = reflector.AssemblyModel;
            Logger?.Log("DLL loaded!", LogLevel.Information);
        }

        #endregion

        #endregion
    }
}