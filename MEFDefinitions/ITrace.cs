namespace MEFDefinitions
{
    public interface ITrace
    {
        void Write( string message );
        void WriteLine( string message, string category );
        LogLevel Level { get; set; }
    }
}