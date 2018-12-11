using System.Data.Entity;
using DataBaseSerializationSurrogates.MetadataSurrogates;

namespace DataBaseRepository
{
    public class ReflectorDbContext : DbContext
    {
        #region DbSets

        public DbSet<AssemblyMetadataSurrogate> AssemblyModels { get; set; }
        public DbSet<NamespaceMetadataSurrogate> NamespaceModels { get; set; }
        public DbSet<TypeMetadataSurrogate> TypeModels { get; set; }
        public DbSet<EventMetadataSurrogate> EventModels { get; set; }
        public DbSet<FieldMetadataSurrogate> FieldModels { get; set; }
        public DbSet<MethodMetadataSurrogate> MethodModels { get; set; }
        public DbSet<ParameterMetadataSurrogate> ParameterModels { get; set; }
        public DbSet<PropertyMetadataSurrogate> PropertiesModels { get; set; }

        #endregion

        #region Constructor

        public ReflectorDbContext() : base( "ReflectorDb" )
        {
        }

        #endregion

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            DefineEntitiesKeys( modelBuilder );
//            DefineRelations( modelBuilder );
        }

        #region Privates

        private void DefineEntitiesKeys( DbModelBuilder modelBuilder )
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

        private void DefineRelations( DbModelBuilder modelBuilder )
        {
            DefineNamespaceRelations( modelBuilder );
            DefineTypeRelations( modelBuilder );
            DefineMethodRelations( modelBuilder );
            DefineEventRelations( modelBuilder );
            DefineFieldRelations( modelBuilder );
            DefineParameterRelations( modelBuilder );
            DefinePropertyRelations( modelBuilder );
        }

        private void DefinePropertyRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasOptional( p => p.TypeMetadata )
                .WithMany( t => t.PropertyTypeMetadatas )
                .HasForeignKey( p => p.TypeForeignId )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasOptional( p => p.Getter )
                .WithMany( m => m.PropertyGettersSurrogates )
                .HasForeignKey( p => p.GetterId )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasOptional( p => p.Setter )
                .WithMany( m => m.PropertySettersSurrogates )
                .HasForeignKey( p => p.SetterId )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<PropertyMetadataSurrogate>()
                .HasMany( p => p.PropertyAttributes )
                .WithMany( t => t.PropertyAttributesTypes );
        }

        private void DefineParameterRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<ParameterMetadataSurrogate>()
                .HasOptional( p => p.TypeMetadata )
                .WithMany( t => t.ParameterMetadataSurrogates )
                .HasForeignKey( p => p.TypeForeignId )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<ParameterMetadataSurrogate>()
                .HasMany( p => p.ParameterAttributes )
                .WithMany( t => t.ParameterAttributeSurrogates );
        }

        private void DefineFieldRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<FieldMetadataSurrogate>()
                .HasOptional( f => f.TypeMetadata )
                .WithMany( t => t.FieldTypeSurrogates )
                .HasForeignKey( f => f.TypeForeignId )
                .WillCascadeOnDelete( false );
        }

        private void DefineEventRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasMany( e => e.TypeEventMetadataSurrogate )
                .WithMany( t => t.Events );

            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.TypeMetadata )
                .WithMany( t => t.EventTypeSurrogates )
                .HasForeignKey(e => e.TypeForeignId  )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.AddMethodMetadata )
                .WithMany( m => m.EventAddMethodMetadataSurrogates )
                .HasForeignKey( e => e.AddMethodId )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.RaiseMethodMetadata )
                .WithMany( m => m.EventRaiseMethodMetadataSurrogates )
                .HasForeignKey( e => e.RaiseMethodId  )
                .WillCascadeOnDelete( false );
            modelBuilder.Entity<EventMetadataSurrogate>()
                .HasOptional( e => e.RemoveMethodMetadata )
                .WithMany( m => m.EventRemoveMethodMetadataSurrogates )
                .HasForeignKey( e => e.RemoveMethodId )
                .WillCascadeOnDelete( false );
        }

        private void DefineMethodRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasOptional( m => m.ReturnType )
                .WithMany( t => t.ReturnTypeSurrogates )
                .HasForeignKey( m => m.ReturnTypeId )
                .WillCascadeOnDelete( false );

            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasMany( m => m.TypeMethodMetadataSurrogates )
                .WithMany( t => t.Methods );
            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasMany( m => m.TypeConstructorMetadataSurrogates )
                .WithMany( t => t.Constructors );
            modelBuilder.Entity<MethodMetadataSurrogate>()
                .HasMany( m => m.Parameters )
                .WithMany( p => p.MethodParametersSurrogates );
        }

        private void DefineTypeRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<TypeMetadataSurrogate>()
                .HasOptional( t => t.NamespaceMetadataSurrogate )
                .WithMany( n => n.Types )
                .HasForeignKey( t => t.NamespaceForeignId )
                .WillCascadeOnDelete( false );

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
        }

        private void DefineNamespaceRelations( DbModelBuilder modelBuilder )
        {
            modelBuilder.Entity<NamespaceMetadataSurrogate>()
                .HasRequired( n => n.AssemblyMetadataSurrogate )
                .WithMany( a => a.Namespaces )
                .HasForeignKey( n => n.AssemblyForeignId )
                .WillCascadeOnDelete( false );
        } 

        #endregion
    }
}