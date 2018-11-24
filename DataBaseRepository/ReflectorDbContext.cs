using System.Data.Entity;
using DataBaseSerializationSurrogates.MetadataSurrogates;

namespace DataBaseRepository
{
    public class ReflectorDbContext : DbContext
    {
        public DbSet<AssemblyMetadataSurrogate> Assemblies { get; set; }
        public DbSet<NamespaceMetadataSurrogate> Namespaces { get; set; }
        public DbSet<TypeMetadataSurrogate> Types { get; set; }
        public DbSet<EventMetadataSurrogate> Events { get; set; }
        public DbSet<FieldMetadataSurrogate> Fields { get; set; }
        public DbSet<MethodMetadataSurrogate> Methods { get; set; }
        public DbSet<ParameterMetadataSurrogate> Parameters { get; set; }
        public DbSet<PropertyMetadataSurrogate> Properties { get; set; }
    }
}