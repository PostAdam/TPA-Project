using System;
using System.IO;
using ViewModel;

namespace CLI
{
    class Program
    {
        //TODO: move to another class, refactor code
        static void Main()
        {
            ConfigureConsoleWindow();
            string command;
            MainViewModel viewModel = InitViewModel();
            DllViewer dllViewer = new DllViewer();
            PrintMenu();
            do
            {
                command = Console.ReadLine();

                switch (command)
                {
                    case "open":
                        try
                        {
                            Console.WriteLine();
                            Console.WriteLine(@"Provide filename:");
                            Console.Write(">");
                            string filename = GetFilename();
                            viewModel.Init(filename);
                            dllViewer.RootNodes = viewModel.Items;
                            dllViewer.DisplayTree();
                        }
                        catch (FileNotFoundException fnfe)
                        {
                            Console.WriteLine(fnfe.Message);
                        }

                        break;
                    case "save":
                        viewModel.Save();
                        Console.WriteLine("Serialized to Test.xml");
                        break;
                    case "read":
                        Console.WriteLine();
                        Console.WriteLine(@"Read filename:");
                        Console.Write(">");
                        string fn = Console.ReadLine();
                        viewModel.ReadFromFile(fn);
                        dllViewer.RootNodes = viewModel.Items;
                        dllViewer.DisplayTree();
                        break;
                    case "help":
                        PrintMenu();
                        break;
                    case "ls":
                        Console.WriteLine();
                        Console.WriteLine("Directory list:");
                        foreach (var directory in Directory.EnumerateDirectories(Directory.GetCurrentDirectory()))
                        {
                            Console.WriteLine(Path.GetDirectoryName(directory));
                        }

                        foreach (var file in Directory.GetFiles(Directory.GetCurrentDirectory()))
                        {
                            Console.WriteLine(Path.GetFileName(file));
                        }

                        Console.WriteLine();
                        break;
                    case "cd":
                        Console.WriteLine();
                        Console.WriteLine("Move to:");
                        Console.Write(">");
                        string dir = Directory.GetCurrentDirectory();
                        string f = Console.ReadLine();
                        Directory.SetCurrentDirectory(dir + "\\" + f);
                        break;
                    case "exit":
                        Console.WriteLine(@"Have an awesome day!");
                        break;
                    default:
                        Console.WriteLine(@"Not an option");
                        break;
                }
            } while (command != "exit");


            Console.ReadKey();
        }

        private static MainViewModel InitViewModel()
        {
            MainViewModel viewmodel = new MainViewModel();
            return viewmodel;
        }

        private static void ConfigureConsoleWindow()
        {
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = Console.LargestWindowWidth - Console.LargestWindowWidth / 3;
        }

        private static void PrintMenu()
        {
            Console.WriteLine("Press [open] to open dll/exe.");
            Console.WriteLine("Press [save] to serialize model to xml.");
            Console.WriteLine("Press [read] to read from xml.");
            Console.WriteLine("Press [help] to see this help.");
            Console.WriteLine("Press [ls] for unix ls command.");
            Console.WriteLine("Press [cd] for unix cd command.");
            Console.WriteLine("Press [exit] to quit.");
        }

        private static string GetFilename()
        {
            string filename = Console.ReadLine();
            string directory = Directory.GetCurrentDirectory();
            string fullPath = directory + "\\" + filename;
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File {fullPath} doesn't exist.");
            }

            return fullPath;
        }
    }
}