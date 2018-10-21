using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Input;
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
        private static readonly TraceSource myListener = new TraceSource("Log");

        #region Constructor

        public MainViewModel(Logger logger)
        {
            myListener.TraceInformation("PLS LOG ME");
            myListener.Flush();
            Items = new ObservableCollection<MetadataViewModel>();
            ClickSave = new DelegateCommand(Save);
            Logger = logger;
        }

        #endregion

        #region DataContext

        public static Logger Logger { get; private set; }
        public ObservableCollection<MetadataViewModel> Items { get; set; }

        #endregion

        #region Private

        public ICommand ClickSave { get; }
        internal AssemblyMetadata AssemblyMetadata;

        #region Methods

        public void Save()
        {
            Logger.Log("Starting serializaton process.", LogLevel.Information);
            DataContractSerializer serializer = new DataContractSerializer(AssemblyMetadata.GetType());
            using (FileStream stream = File.Create(@"Test.Xml"))
            {
                serializer.WriteObject(stream, AssemblyMetadata);
            }

            Logger.Log("Serializaton completed!", LogLevel.Information);
        }

        public void Open(string fileName)
        {
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
            DataContractSerializer serializer = new DataContractSerializer(typeof(AssemblyMetadata));
            using (FileStream stream = File.OpenRead(filename))
            {
                AssemblyMetadata data = (AssemblyMetadata) serializer.ReadObject(stream);
                AddClassesToDirectory(data);
                InitTreeView(data);
            }
        }

        internal void AddClassesToDirectory(AssemblyMetadata assemblyMetadata)
        {
            Logger.Log("Adding classes to directory.", LogLevel.Information);
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

            Logger.Log("Classes added to directory!", LogLevel.Information);
        }

        internal void InitTreeView(AssemblyMetadata assemblyMetadata)
        {
            Logger.Log("Initializing treeView.", LogLevel.Information);
            MetadataViewModel metadataViewModel = new AssemblyMetadataViewModel(assemblyMetadata);
            Items.Add(metadataViewModel);
            Logger.Log("TreeView initialized!", LogLevel.Information);
        }

        public void Init(string filename)
        {
            LoadDll(filename);
            InitTreeView(AssemblyMetadata);
        }

        internal void LoadDll(string path)
        {
            Logger.Log("Loading DLL.", LogLevel.Information);
            Reflector reflector = new Reflector();
            reflector.Reflect(path);
            AssemblyMetadata = reflector.AssemblyModel;
            Logger.Log("DLL loaded!", LogLevel.Information);
        }

        #endregion

        #endregion
    }
}