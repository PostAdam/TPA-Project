using System;
using System.Collections.Generic;
using ViewModel.MetadataBaseViewModels;

namespace CLI
{
    public class DllViewer
    {
        #region Constructor

        public DllViewer(int maxLevelVisible = 5)
        {
            MaxLevelVisible = maxLevelVisible;
        }

        public DllViewer(IList<MetadataBaseViewModel> rootNodes, int maxLevelVisible = 5)
        {
            MaxLevelVisible = maxLevelVisible;
            RootNodes = rootNodes;
        }

        #endregion

        #region Public

        public int MaxLevelVisible { get; set; }
        public IList<MetadataBaseViewModel> RootNodes { get; set; }

        public void DisplayTree()
        {
            char key = ' ';
            int currentDepth = 0;
            _pathFromRoot.Clear();

            while (key != 'q')
            {
                Console.Clear();

                int startLevel = 0;
                _currentLevelNodes = RootNodes;

                if (currentDepth > MaxLevelVisible)
                {
                    startLevel = currentDepth - MaxLevelVisible;
                    SkipBeginningLevels(startLevel);
                }

                PrintAllNodesAbovePath(startLevel, currentDepth);

                PrintCurrentLevelWithOptions(currentDepth - startLevel);

                PrintAllNodesBelowPath();

                key = GetNodeChar(_currentLevelNodes.Count);
                if (key == 65)
                {
                    Console.Clear();
                    Console.WriteLine("You're back in menu!");
                    return;
                }
               
                currentDepth = (key == FoldKey) ? PreviousLevel(currentDepth) : NextLevel(currentDepth, key);
            }
        }

        #endregion

        #region Private 

        #region Fields

        private const char FoldKey = '\ufffc';

        private IList<MetadataBaseViewModel> _currentLevelNodes;
        private readonly List<int> _pathFromRoot = new List<int>();
        private readonly Stack<List<MetadataBaseViewModel>> _nodesBelowPath = new Stack<List<MetadataBaseViewModel>>();

        #endregion

        #region Methods

        private void PrintAllNodesAbovePath(int startLevel, int depth)
        {
            for (int currentLevel = startLevel; currentLevel < depth; currentLevel++)
            {
                PrintNodesAbovePath(startLevel, currentLevel);

                if (_pathFromRoot.Count > currentLevel)
                {
                    _currentLevelNodes =
                        ExpandNodeAndGetItsChildren(currentLevel);
                }
            }
        }

        private IList<MetadataBaseViewModel> ExpandNodeAndGetItsChildren(int level)
        {
            _currentLevelNodes[_pathFromRoot[level]].IsExpanded = true;
            return _currentLevelNodes[_pathFromRoot[level]].Children;
        }

        private void SkipBeginningLevels(int skipNumber)
        {
            for (int i = 0; i < skipNumber; i++)
            {
                _currentLevelNodes = _currentLevelNodes[_pathFromRoot[i]].Children;
            }

            Console.WriteLine(@"[...]");
        }

        private void PrintCurrentLevelWithOptions(int indentSize)
        {
            for (int i = 0; i < _currentLevelNodes.Count; i++)
            {
                PrintIndent(indentSize);
                PrintOptionChar(i);
                Console.WriteLine(_currentLevelNodes[i].FullName);
            }
        }

        private void PrintNodesAbovePath(int startingLevel, int currentLevel)
        {
            List<MetadataBaseViewModel> remainingNodes = new List<MetadataBaseViewModel>();

            for (var index = 0; index < _currentLevelNodes.Count; index++)
            {
                MetadataBaseViewModel node = _currentLevelNodes[index];
                if (_pathFromRoot.Count > currentLevel &&
                    _pathFromRoot[currentLevel] >= index)
                {
                    PrintIndent(currentLevel - startingLevel);
                    Console.WriteLine(node.FullName);
                }
                else
                {
                    remainingNodes.Add(node);
                }

                if (index == _currentLevelNodes.Count - 1)
                {
                    _nodesBelowPath.Push(remainingNodes);
                    remainingNodes = new List<MetadataBaseViewModel>();
                }
            }
        }

        private void PrintIndent(int indentSize)
        {
            for (int i = 0; i < indentSize; i++)
                Console.Write(@"  ");
        }

        private void PrintOptionChar(int optionSymbol)
        {
            Console.Write($@"[{(char) (optionSymbol + 48)}]");
        }

        private void PrintAllNodesBelowPath()
        {
            int cursorPosition = Console.CursorTop;
            while (_nodesBelowPath.Count > 0)
            {
                List<MetadataBaseViewModel> nodes = _nodesBelowPath.Pop();
                foreach (var node in nodes)
                {
                    for (int j = _nodesBelowPath.Count; j > 0; j--)
                        Console.Write(@"  ");
                    Console.WriteLine(node.FullName);
                }
            }

            Console.CursorTop = cursorPosition - _currentLevelNodes.Count;
        }

        private char GetNodeChar(int childNumber)
        {
            char key;
            do
            {
                key = Console.ReadKey(true).KeyChar;
                key -= '0';
            } while ((key < 0 || key >= childNumber) && key != FoldKey && key != 65);

            return key;
        }

        private int NextLevel(int level, char key)
        {
            _pathFromRoot.Add(key);
            level += 1;
            return level;
        }

        private int PreviousLevel(int level)
        {
            if (level > 0)
            {
                level--;
                _pathFromRoot.RemoveAt(_pathFromRoot.Count - 1);
            }

            return level;
        }

        #endregion

        #endregion
    }
}