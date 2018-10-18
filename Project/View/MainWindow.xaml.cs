using System.Diagnostics;
using System.Windows;
namespace Project
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Logger logger = new Logger( new TextWriterTraceListener( "Logs.log" ) );
            DataContext = new ViewModel.ViewModel( logger );
        }
    }
}
