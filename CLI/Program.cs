using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Project.ViewModel;

namespace CLI
{
    class Program
    {
        const char FoldKey = '\ufffc';

        static void Main()
        {
            ConfigureConsoleWindow();
            List<MetadataViewModel> rootMetadata = InitViewModel().ToList();

            char key = ' ';
            int level = 0;
            Stack<List<MetadataViewModel>> reminingLevelNodes = new Stack<List<MetadataViewModel>>();
            List<int> pathFromRoot = new List<int>();


            while (key != 'q')
            {
                Console.Clear();
                IList<MetadataViewModel> actualAssemblyModel = rootMetadata;
                List<MetadataViewModel> reminingNodes = new List<MetadataViewModel>();
                int start = 0;

                if (level > 5)
                {
                    start = level - 5;
                    SkipBeginningLevels(start, pathFromRoot, ref actualAssemblyModel);
                }

                for (int j = start; j <= level; j++)
                {
                    for (var index = 0; index < actualAssemblyModel.Count; index++)
                    {
                        var item = actualAssemblyModel[index];
                        if (pathFromRoot.Count > j && pathFromRoot[j] >= index)
                        {
                            PrintIndent(j - start);
                            Console.WriteLine(item.Name);
                        }
                        else
                        {
                            reminingNodes.Add(item);
                        }

                        if (j == level)
                        {
                            PrintCurrentLevelWithOptions(j - start, item.Name, index);
                        }
                        else
                        {
                            if (index == actualAssemblyModel.Count - 1)
                            {
                                reminingLevelNodes.Push(reminingNodes);
                                reminingNodes = new List<MetadataViewModel>();
                            }
                        }
                    }

                    if (pathFromRoot.Count > j)
                    {
                        actualAssemblyModel[pathFromRoot[j]].IsExpanded = true;
                        actualAssemblyModel = actualAssemblyModel[pathFromRoot[j]].Child;
                    }
                }

                PrintRemainingNodes(reminingLevelNodes);

                key = GetNodeChar(actualAssemblyModel.Count);

                level = (key == FoldKey) ? PreviousLevel(level, pathFromRoot) : NextLevel(level, pathFromRoot, key);
            }

            Console.ReadKey();
        }

        private static void ConfigureConsoleWindow()
        {
            Console.WindowHeight = Console.LargestWindowHeight - 10;
            Console.WindowWidth = Console.LargestWindowWidth - Console.LargestWindowWidth / 2;
        }

        private static IEnumerable<MetadataViewModel> InitViewModel()
        {
            ViewModel viewmodel = new ViewModel();
            viewmodel.LoadDll(Directory.GetCurrentDirectory() + "\\ClassLibrary1.dll");
            viewmodel.InitTreeView(viewmodel.AssemblyMetadata);
            return viewmodel.Items;
        }

        private static void SkipBeginningLevels(int skipNumber, List<int> pathFromRoot,
            ref IList<MetadataViewModel> metadataModel)
        {
            for (int i = 0; i < skipNumber; i++)
            {
                metadataModel = metadataModel[pathFromRoot[i]].Child;
            }

            Console.WriteLine("[...]");
        }

        private static void PrintCurrentLevelWithOptions(int indentSize, string nodeName, int optionSymbol)
        {
            PrintIndent(indentSize);
            PrintOptionChar(optionSymbol);
            Console.WriteLine(nodeName);
        }

        private static void PrintIndent(int indentSize)
        {
            for (int i = 0; i < indentSize; i++)
                Console.Write("  ");
        }

        private static void PrintOptionChar(int optionSymbol)
        {
            Console.Write("[" + (char) (optionSymbol + 48) + "]");
        }

        private static void PrintRemainingNodes(Stack<List<MetadataViewModel>> remainingNodesStack)
        {
            while (remainingNodesStack.Count > 0)
            {
                List<MetadataViewModel> nodes = remainingNodesStack.Pop();
                for (int i = 0; i < nodes.Count; i++)
                {
                    for (int j = remainingNodesStack.Count; j > 0; j--)
                        Console.Write("  ");
                    Console.WriteLine(nodes[i].Name);
                }
            }
        }

        private static char GetNodeChar(int childNumber)
        {
            char key = ' ';
            do
            {
                key = Console.ReadKey(true).KeyChar;
                key -= '0';
            } while ((key < 0 || key >= childNumber) && key != FoldKey);

            return key;
        }

        private static string GetFilename()
        {
            string filename = Console.ReadLine();
            string directory = Directory.GetCurrentDirectory();
            if (File.Exists(directory + filename))
            {
                Console.WriteLine(@"File {directory}/{filename} doesn't exist.");
                return " ";
            }

            return directory + filename;
        }

        private static int NextLevel(int level, List<int> pathFromRoot, char key)
        {
            pathFromRoot.Add(key);
            level += 1;
            return level;
        }

        private static int PreviousLevel(int level, List<int> nodesOpened)
        {
            if (level > 0)
            {
                level--;
                nodesOpened.RemoveAt(nodesOpened.Count - 1);
            }

            return level;
        }
    }
}