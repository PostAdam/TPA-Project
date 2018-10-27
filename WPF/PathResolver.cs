using System.IO;
using Microsoft.Win32;
using ViewModel;

namespace WPF
{
    public class PathResolver : IPathResolver
    {
        public string OpenFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library (*.dll)|*.dll",
                RestoreDirectory = true
            };
            if ( openFileDialog.ShowDialog() == true )
            {
                return ( openFileDialog.FileName );
            }

            return null;
        }

        public string SaveFilePath()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Dynamic Library (*.xml)|*.xml",
                RestoreDirectory = true
            };
            if ( saveFileDialog.ShowDialog() == true )
            {
                return ( saveFileDialog.FileName );
            }

            return null;

        }

        public string ReadFilePath()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Dynamic Library (*.xml)|*.xml",
                RestoreDirectory = true
            };
            if ( openFileDialog.ShowDialog() == true )
            {
                return ( openFileDialog.FileName );
            }

            return null;
        }
    }
}