using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using MEFDefinitions;
using Microsoft.Extensions.Logging;

namespace Model
{
    [Export( typeof(ITrace) )]
    [ExportMetadata( "destination", "file" )]
    public class TextLogger : ITrace
    {
        private TraceListener _traceListener;
        public LogLevel Level { get; set; }

        public TextLogger()
        {
            LoadLoggerConfiguration();
        }

        public void Write( string message )
        {
            _traceListener.WriteLine( message );
            _traceListener.Flush();
        }

        public void WriteLine( string message, string category )
        {
            LogLevel logLevelTreshold = (LogLevel) Enum.Parse( typeof(LogLevel), category );
            if (logLevelTreshold <= Level)
            {
                _traceListener.WriteLine( message, category.ToString() );
                _traceListener.Flush();
            }
        }

        private void LoadLoggerConfiguration()
        {
            string filename = ConfigurationManager.AppSettings["filename"];
            _traceListener = new TextWriterTraceListener( filename );
            LoadLogLevel();
        }

        private void LoadLogLevel()
        {
            string logLevel = ConfigurationManager.AppSettings["logLevel"];

            if (int.TryParse( logLevel, out int level ))
            {
                Level = (LogLevel) level;
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