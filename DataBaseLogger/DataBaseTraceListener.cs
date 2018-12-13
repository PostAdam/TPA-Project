using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading.Tasks;
using MEFDefinitions;

namespace DataBaseLogger
{
    [Export( typeof( ITrace ) )]
    [ExportMetadata( "destination", "db" )]
    class DataBaseTraceListener : ITrace
    {
        #region Constructor

        public DataBaseTraceListener()
        {
            _connectionString = GetConnectionString();
        }

        #endregion

        public LogLevel Level { get; set; }

        public async Task Write( string message )
        {
            await WriteLine( message, null );
        }

        public async Task WriteLine( string message, string category )
        {
            LogLevel logLevelTreshold = ( LogLevel ) Enum.Parse( typeof( LogLevel ), category );
            if ( logLevelTreshold <= Level )
            {
                await SaveLogEntry( message, category );
            }
        }

        #region Privates

        private readonly string _connectionString;

        private async Task SaveLogEntry( string message, string category )
        {
            using ( SqlConnection sqlConnection = new SqlConnection( _connectionString ) )
            {
                using ( SqlCommand sqlCommand = new SqlCommand() )
                {
                    DateTime time = DateTime.Now;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = @"INSERT INTO LOGS_T ( TIME, MESSAGE ) 
                                               VALUES( @time, @message )";

                    sqlCommand.Parameters.AddWithValue( "@time", time );
                    sqlCommand.Parameters.AddWithValue( "@message", message );

                    try
                    {
                        sqlConnection.Open();
                        await sqlCommand.ExecuteNonQueryAsync();
                    }
                    catch ( SqlException e )
                    {
                        Console.WriteLine( e );
                    }
                }
            }
        }

        private string GetConnectionString()
        {
            Configuration appConfig =
                ConfigurationManager.OpenExeConfiguration( Assembly.GetExecutingAssembly().Location );
            string connectionString = appConfig.ConnectionStrings
                .ConnectionStrings[ "DataBase.Properties.Settings.DataBaseConnectionString" ].ConnectionString;
            return connectionString;
        } 

        #endregion
    }
}