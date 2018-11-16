using System.Data.Entity;

namespace DataBaseRepository
{
    public class ReflectorDbContext<T> : DbContext where T : class
    {
        public DbSet<T> Assemblies { get; set; }
    }
}