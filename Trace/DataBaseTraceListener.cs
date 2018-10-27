using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using MEFDefinitions;

namespace Trace
{
    [Export( typeof( ITrace ) )]
    [ExportMetadata( "destination", "db" )]
    class DataBaseTraceListener : ITrace
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _logLevelToTableNameDictionary;
        public LogLevel Level { get; set; }

        public DataBaseTraceListener()
        {
            _connectionString = GetConnectionString();
            _logLevelToTableNameDictionary = GetLogLevelToTableNameDictionary();
        }

        public void Write( string message )
        {
            this.WriteLine( message, null );
        }

        public void WriteLine( string message, string category )
        {
            LogLevel logLevelTreshold = (LogLevel) Enum.Parse( typeof(LogLevel), category );
            if (logLevelTreshold <= Level)
            {
                SaveLogEntry( message, category );
            }
        }

        private void SaveLogEntry( string message, string category )
        {
            using (SqlConnection sqlConnection = new SqlConnection( _connectionString ))
            {
                using (SqlCommand sqlCommand = new SqlCommand())
                {
                    DateTime time = DateTime.Now;
                    sqlCommand.Connection = sqlConnection;
                    sqlCommand.CommandType = CommandType.Text;
                    string tableName = _logLevelToTableNameDictionary[ category ];
                    sqlCommand.CommandText = $@"INSERT INTO {tableName} ( TIME, MESSAGE ) 
                                                VALUES( @time, @message )";

                    sqlCommand.Parameters.AddWithValue( "@time", time );
                    sqlCommand.Parameters.AddWithValue( "@message", message );

                    try
                    {
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                    }
                    catch (SqlException e)
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
                .ConnectionStrings["Model.Properties.Settings.LoggingDataBaseConnectionString"].ConnectionString;
            return connectionString;
        }

        private Dictionary<string, string> GetLogLevelToTableNameDictionary()
        {
            Dictionary<string, string> logLevelToTableNameDictionary = new Dictionary<string, string>();
            foreach (LogLevel logLevel in Enum.GetValues( typeof(LogLevel) ))
            {
                string logLevelName = logLevel.ToString();
                string logLevelTableName = logLevelName.ToUpper() + "_T";
                logLevelToTableNameDictionary.Add( logLevelName, logLevelTableName );
            }

            return logLevelToTableNameDictionary;
        }
    }
}