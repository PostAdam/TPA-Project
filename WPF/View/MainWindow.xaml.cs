using System;
using System.Diagnostics;
using System.Windows;
using Microsoft.Win32;
using ViewModel;

namespace WPF.View
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; set; }

        public MainWindow()
        {
            PresentationTraceSources.Refresh();
            InitializeComponent();
            Logger logger = new Logger(new ConsoleTraceListener());
            ViewModel = new MainViewModel(logger);
            DataContext = ViewModel;
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