using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ViewModel
{
    public class Logger
    {
        private readonly TraceListener _traceListener;

        public Logger(TraceListener traceListener)
        {
            _traceListener = traceListener;
        }

        public void Log(string message)
        {
            _traceListener.WriteLine(message);
            _traceListener.Flush();
        }

        public void Log(string message, LogLevel logLevel)
        {
            _traceListener.WriteLine(message, logLevel.ToString());
            _traceListener.Flush();
        }
    }
}
