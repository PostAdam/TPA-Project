using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Project;
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
            Logger logger = new Logger( new TextWriterTraceListener( "Logs.log" ) );
            ViewModel viewmodel = new ViewModel( logger );
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