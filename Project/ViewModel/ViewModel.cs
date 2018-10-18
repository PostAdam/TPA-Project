using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using Project.Model.Reflection;
using Project.Model.Reflection.Model;

namespace Project.ViewModel
{
    public class ViewModel : BaseViewModel
    {
        #region Constructor

        public ViewModel()
        {
            Items = new ObservableCollection<MetadataViewModel>();
            ClickOpen = new DelegateCommand( Open );
            ClickSave = new DelegateCommand( Save );
            ClickRead = new DelegateCommand( Read );
        }

        #endregion

        #region DataContext

        public ObservableCollection<MetadataViewModel> Items { get; set; }

        public ICommand ClickOpen { get; }
        public ICommand ClickSave { get; }
        public ICommand ClickRead { get; }

        #endregion

        #region Private

        internal AssemblyMetadata AssemblyMetadata;

        #region Methods

        internal void Save()
        {
            DataContractSerializer serializer = new DataContractSerializer( AssemblyMetadata.GetType() );
            using (FileStream stream = File.Create( @"..\Test.Xml" ))
            {
                serializer.WriteObject( stream, AssemblyMetadata );
            }

            MessageBox.Show( "Serialization Completed!" );
        }

        private void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library (*.dll)|*.dll",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                LoadDll( openFileDialog.FileName );
                InitTreeView( AssemblyMetadata );
            }
        }

        private void Read()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML File (*.xml)|*.xml",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                DataContractSerializer serializer = new DataContractSerializer( typeof(AssemblyMetadata) );
                using (FileStream stream = File.OpenRead( openFileDialog.FileName ))
                {
                    AssemblyMetadata data = (AssemblyMetadata) serializer.ReadObject( stream );
                    AddClassesToDirectory( data );

                    InitTreeView( data );
                }
            }
        }

        #endregion

        #region Help Methods

        internal void AddClassesToDirectory( AssemblyMetadata assemblyMetadata )
        {
            foreach (NamespaceMetadata dataNamespace in assemblyMetadata.Namespaces)
            {
                foreach (TypeMetadata type in dataNamespace.Types)
                {
                    if (TypesDictionary.ReflectedTypes.ContainsKey( type.TypeName ) == false)
                    {
                        TypesDictionary.ReflectedTypes.Add( type.TypeName, type );
                    }
                }
            }
        }

        internal void InitTreeView( AssemblyMetadata assemblyMetadata )
        {
            MetadataViewModel metadataViewModel = new AssemblyMetadataViewModel( assemblyMetadata );
            Items.Add( metadataViewModel );
        }

        internal void LoadDll( string path )
        {
            Reflector reflector = new Reflector();
            reflector.Reflect( path );
            AssemblyMetadata = reflector.AssemblyModel;
        }

        #endregion

        #endregion
    }
}