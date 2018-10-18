using System;
using System.Collections.Generic;
using System.IO;
using Project.ViewModel;

namespace CLI
{
    class Program
    {
        static void Main()
        {
            ConfigureConsoleWindow();
            DllViewer dllViewer = new DllViewer(InitViewModel());
            dllViewer.DisplayTree();
            Console.ReadKey();
        }

        private static IList<MetadataViewModel> InitViewModel()
        {
            ViewModel viewmodel = new ViewModel();
            viewmodel.LoadDll(Directory.GetCurrentDirectory() + "\\ClassLibrary1.dll");
            viewmodel.InitTreeView(viewmodel.AssemblyMetadata);
            return viewmodel.Items;
        }
        private static void ConfigureConsoleWindow()
        {
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = Console.LargestWindowWidth - Console.LargestWindowWidth / 2;
        }
    }
}