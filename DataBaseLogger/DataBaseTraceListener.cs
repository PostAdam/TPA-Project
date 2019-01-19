using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using MEFDefinitions;

namespace DataBaseLogger
{
    [Export( typeof( ITrace ) )]
    [ExportMetadata( "destination", "db" )]
    class DataBaseTraceListener : ITrace
    {
        public LogLevel Level { get; set; }

        public async Task Write( string message )
        {
            await WriteLine( message, null );
        }

        public async Task WriteLine( string message, string category )
        {
            LogLevel logLevelTreshold = (LogLevel) Enum.Parse( typeof( LogLevel ), category );
            if ( logLevelTreshold <= Level )
            {
                await SaveLogEntry( message, category );
            }
        }

        #region Privates

        private async Task SaveLogEntry( string message, string category )
        {
            using ( LoggerDbContext dbContext = new LoggerDbContext() )
            {
                Log log = new Log
                {
                    Time = DateTime.Now,
                    Category = category,
                    Message = message,
                };

                dbContext.Logs.Add( log );
                await dbContext.SaveChangesAsync();
            }
        }

        #endregion
    }
}