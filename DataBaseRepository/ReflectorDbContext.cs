using System.Collections.Generic;
using System.Data.Entity;
using DataBaseSerializationSurrogates.MetadataSurrogates;

namespace DataBaseRepository
{
    public class ReflectorDbContext : DbContext
    {
        public DbSet<AssemblyMetadataSurrogate> AssemblyModels { get; set; }
        public DbSet<NamespaceMetadataSurrogate> NamespaceModels { get; set; }
        public DbSet<TypeMetadataSurrogate> TypeModels { get; set; }
        public DbSet<EventMetadataSurrogate> EventModels { get; set; }
        public DbSet<FieldMetadataSurrogate> FieldModels { get; set; }
        public DbSet<MethodMetadataSurrogate> MethodModels { get; set; }
        public DbSet<ParameterMetadataSurrogate> ParameterModels { get; set; }
        public DbSet<PropertyMetadataSurrogate> PropertiesModels { get; set; }

        public ReflectorDbContext() : base( "ReflectorDb" )
        {
        }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            DefineEntitiesKeys( modelBuilder );
            DefineRelations( modelBuilder );
        }

        private static void DefineRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<NamespaceMetadataSurrogate>()
                .HasRequired( n => n.AssemblyMetadataSurrogate )
                .WithMany( a => a.Namespaces );


            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasOptional( t => t.NamespaceMetadataSurrogate )
                .WithMany( n => n.Types );
            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasMany( t => t.FieldAttributesType )
                .WithMany( f => f.FieldAttributes );
            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasMany( t => t.GenericArgumentsTypeSurrogates )
                .WithMany( m => m.GenericArguments );
            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasMany( t => t.MethodAttributesTypeSurrogates )
                .WithMany( m => m.MethodAttributes );
            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasMany( t => t.EventAttributesSurrogates )
                .WithMany( e => e.EventAttributes );
            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasMany( t => t.Fields )
                .WithMany( f => f.TypeFieldsSurrogates );
            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasMany( t => t.Properties )
                .WithMany( p => p.TypePropertiesSurrogates );


            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasOptional( m => m.ReturnType )
                .WithMany( t => t.ReturnTypeSurrogates );
            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasMany( m => m.TypeMethodMetadataSurrogates )
                .WithMany( t => t.Methods );
            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasMany( m => m.TypeConstructorMetadataSurrogates )
                .WithMany( t => t.Constructors );
            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasMany( m => m.Parameters )
                .WithMany( p => p.MethodParametersSurrogates );


            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasMany( e => e.TypeEventMetadataSurrogate )
                .WithMany( t => t.Events );


            modelBuilder.Entity<FieldMetadataSurrogate>()
                .HasOptional( f => f.TypeMetadata )
                .WithMany( t => t.FieldTypeSurrogates );


            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.TypeMetadata )
                .WithMany( t => t.EventTypeSurrogates );
            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.AddMethodMetadata )
                .WithMany( m => m.EventAddMethodMetadataSurrogates );
            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.RaiseMethodMetadata )
                .WithMany( m => m.EventRaiseMethodMetadataSurrogates );
            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.RemoveMethodMetadata )
                .WithMany( m => m.EventRemoveMethodMetadataSurrogates );


            modelBuilder.Entity<ParameterMetadataSurrogate>()
                .HasOptional( p => p.TypeMetadata )
                .WithMany( t => t.ParameterMetadataSurrogates );
            modelBuilder.Entity<ParameterMetadataSurrogate>()
                .HasMany( p => p.ParameterAttributes )
                .WithMany( t => t.ParameterAttributeSurrogates );


            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasOptional( p => p.TypeMetadata )
                .WithMany( t => t.PropertyTypeMetadatas );
            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasOptional( p => p.Getter )
                .WithMany( m => m.PropertyGettersSurrogates );
            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasOptional( p => p.Setter )
                .WithMany( m => m.PropertySettersSurrogates );
            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasMany( p => p.PropertyAttributes )
                .WithMany( t => t.PropertyAttributesTypes );
        }

        private static void DefineEntitiesKeys( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<AssemblyMetadataSurrogate>().HasKey( a => a.AssemblyId );
            modelBuilder.Entity<NamespaceMetadataSurrogate>().HasKey( n => n.NamespaceId );
            modelBuilder.Entity<TypeMetadataSurrogate>().HasKey( t => t.TypeId );
            modelBuilder.Entity<EventMetadataSurrogate>().HasKey( e => e.EventId );
            modelBuilder.Entity<FieldMetadataSurrogate>().HasKey( f => f.FieldId );
            modelBuilder.Entity<MethodMetadataSurrogate>().HasKey( m => m.MethodId );
            modelBuilder.Entity<ParameterMetadataSurrogate>().HasKey( p => p.ParameterId );
            modelBuilder.Entity<PropertyMetadataSurrogate>().HasKey( p => p.PropertyId );
        }
    }
}