using System.Data.Entity;

namespace DataBaseLogger
{
    public class LoggerDbContext : DbContext
    {
        #region DbSet

        public DbSet<Log> Logs { get; set; } 

        #endregion

        #region Constructor

        public LoggerDbContext() : base( "name = DataBase.Properties.Settings.LoggerConnectionString" )
        {
            Database.SetInitializer( new CreateDatabaseIfNotExists<LoggerDbContext>() );
        } 

        #endregion
    }
}