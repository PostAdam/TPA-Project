using System;
using System.IO;
using ViewModel;

namespace CLI
{
    public class PathResolver : IPathResolver
    {
        public string OpenFilePath()
        {
            return GetFilename();
        }

        public string SaveFilePath()
        {
            return GetFilename();
        }

        public string ReadFilePath()
        {
            return GetFilename();
        }

        private static string GetFilename()
        {
            string filename = Console.ReadLine();
            string directory = Directory.GetCurrentDirectory();
            string fullPath = directory + "\\" + filename;
            if ( !File.Exists(fullPath) )
            {
                throw new FileNotFoundException($"File {fullPath} doesn't exist.");
            }

            return fullPath;
        }
    }
}