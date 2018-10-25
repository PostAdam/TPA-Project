using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using Microsoft.Extensions.Logging;

namespace Model
{
    [Export( typeof(TraceListener) )]
    [ExportMetadata("destination", "db")]
    class DataBaseTraceListener : TraceListener
    {
        private readonly string _connectionString;
        private readonly Dictionary<string, string> _logLevelToTableNameDictionary;

        public DataBaseTraceListener()
        {
            _connectionString = GetConnectionString();
            _logLevelToTableNameDictionary = GetLogLevelToTableNameDictionary();
        }

        public override void Write( string message )
        {
            this.WriteLine( message, null );
        }

        public override void Write( string message, string category )
        {
            this.WriteLine( message, category );
        }

        public override void WriteLine( string message )
        {
            this.WriteLine( message, null );
        }

        public override void WriteLine( string message, string category )
        {
            SaveLogEntry( message, category );
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
                    sqlCommand.CommandText = @"INSERT INTO @tableName( MESSAGE ) 
                                               VALUES( @time, @message )";

                    string tableName = _logLevelToTableNameDictionary[category];
                    sqlCommand.Parameters.AddWithValue( "@tableName", tableName );
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