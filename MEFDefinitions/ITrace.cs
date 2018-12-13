using System.Threading.Tasks;

namespace MEFDefinitions
{
    public interface ITrace
    {
        Task Write( string message );
        Task WriteLine( string message, string category );
        LogLevel Level { get; set; }
    }
}