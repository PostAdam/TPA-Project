using Microsoft.Extensions.Logging;

namespace MEFDefinitions
{
    public interface ITrace
    {
        void Log(string message);
        void Log(string message, LogLevel logLevel);
        LogLevel Level { get; set; }

    }
}