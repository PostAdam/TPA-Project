﻿using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Windows;
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
            PathResolver pathResolver = new PathResolver();
            ViewModel = new MainViewModel( pathResolver );
            DataContext = ViewModel;
        }

    }
}