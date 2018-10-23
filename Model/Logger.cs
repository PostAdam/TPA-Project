using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Diagnostics;
using MEFDefinitions;
using Microsoft.Extensions.Logging;

namespace Model
{
    [Export(typeof(ITrace))]
    public class Logger : ITrace
    {
        private TraceListener _traceListener;
        private LogLevel _logLevel;
        
        public Logger()
        {
            LoadLoggerConfiguration();
        }

        public void Log(string message)
        {
            _traceListener.WriteLine(message);
            _traceListener.Flush();
        }

        public void Log(string message, LogLevel logLevel)
        {
            if (logLevel <= _logLevel)
            {
                _traceListener.WriteLine(message, logLevel.ToString());
                _traceListener.Flush();
            }
        }

        private void LoadLoggerConfiguration()
        {
            LoadLoggerType();
            LoadLogLevel();
        }

        //TODO: change to DI through method or property and move configuration to wpf/cli ?
        private void LoadLoggerType()
        {
            string loggerType = ConfigurationManager.AppSettings["logger"];

            switch (loggerType)
            {
                case "file":
                {
                    InitTextWriterTraceListener();
                    break;
                }
                case "console":
                {
                    _traceListener = new ConsoleTraceListener();
                    break;
                }
                case "db":
                {
                    throw new NotImplementedException("Tracing to database is not yet implemented.");
                }
                default:
                {
                    InitTextWriterTraceListener();
                    break;
                }
            }
        }

        private void LoadLogLevel()
        {
            string logLevel = ConfigurationManager.AppSettings["logLevel"];

            if (int.TryParse(logLevel, out var level))
            {
                _logLevel = (LogLevel) level;
            }
            else
            {
                _logLevel = LogLevel.Warning;
            }
        }

        private void InitTextWriterTraceListener()
        {
            string filename = ConfigurationManager.AppSettings["filename"];
            filename = string.IsNullOrEmpty(filename) ? "TraceDump.xml" : filename;
            _traceListener = new TextWriterTraceListener(filename);
        }
    }
}