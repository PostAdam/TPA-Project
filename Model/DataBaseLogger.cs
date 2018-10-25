using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using MEFDefinitions;
using Microsoft.Extensions.Logging;

namespace Model
{
    [Export(typeof(ITrace))]
    [ExportMetadata("destination", "db")]
    public class DataBaseLogger : ITrace
    {
        private TraceListener _traceListener;
        private SqlConnection _connection;
        private string _connectionString;
        private DataTable _table;

        public void Log(string message)
        {
            throw new NotImplementedException("db logger");
        }

        public void Log(string message, LogLevel logLevel)
        {
            throw new System.NotImplementedException("db logger");
        }

        public LogLevel Level { get; set; }
    }
}