using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using MEFDefinitions;

namespace FileLogger
{
    [Export( typeof( ITrace ) )]
    [ExportMetadata( "destination", "file" )]
    public class FileLogger : ITrace
    {
        private TraceListener _traceListener;
        public LogLevel Level { get; set; }

        public FileLogger()
        {
            LoadLoggerConfiguration();
        }

        public Task Write( string message )
        {
            Task task = Task.Run( () => WriteLog( message ) );
            return task;
        }

        public Task WriteLine( string message, string category )
        {
            Task task = Task.Run( () => WriteLogLine( message, category ) );
            return task;
        }

        private void WriteLog( string message )
        {
            _traceListener.WriteLine( message );
            _traceListener.Flush();
        }

        private void WriteLogLine( string message, string category )
        {
            LogLevel logLevelTreshold = ( LogLevel ) Enum.Parse( typeof( LogLevel ), category );
            if ( logLevelTreshold <= Level )
            {
                _traceListener.WriteLine( message, category );
                _traceListener.Flush();
            }
        }

        private void LoadLoggerConfiguration()
        {
            string filename = ConfigurationManager.AppSettings[ "logFileName" ];
            _traceListener = new TextWriterTraceListener( filename );
            LoadLogLevel();
        }

        private void LoadLogLevel()
        {
            string logLevel = ConfigurationManager.AppSettings[ "logLevel" ];

            if ( int.TryParse( logLevel, out int level ) )
            {
                Level = ( LogLevel ) level;
            }
            else
            {
                Level = LogLevel.Warning;
            }
        }

        //        private void InitTextWriterTraceListener()
        //        {
        //            string filename = ConfigurationManager.AppSettings["filename"];
        //            filename = string.IsNullOrEmpty(filename) ? "TraceDump.xml" : filename;
        //            _traceListener = new TextWriterTraceListener(filename);
        //        }
    }
}