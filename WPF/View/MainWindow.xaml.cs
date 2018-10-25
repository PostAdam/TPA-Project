using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
using ViewModel;

namespace WPF.View
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }
        private CompositionContainer _container;  
        public MainWindow()
        {
            PresentationTraceSources.Refresh();
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        private void Compose()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(MainWindow).Assembly));

            _container = new CompositionContainer(catalog);
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }

        private void Open(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library (*.dll)|*.dll",
                RestoreDirectory = true
            };
            if (openFileDialog.ShowDialog() == true)
            {
                ViewModel.Open(openFileDialog.FileName);
            }
        }
        private void Read(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "XML File (*.xml)|*.xml",
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ViewModel.Read(openFileDialog.FileName);
            }
        }
    }
}