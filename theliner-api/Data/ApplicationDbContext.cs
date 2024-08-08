using System.Reflection;
using Microsoft.EntityFrameworkCore;
using theliner_api.Models;

namespace theliner_api.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            ApplyGlobalConfigurations(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e =>
                    e.Entity is IAuditableEntity
                    && (e.State == EntityState.Added || e.State == EntityState.Modified)
                );

            foreach (var entityEntry in entries)
            {
                ((IAuditableEntity)entityEntry.Entity).Modified = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((IAuditableEntity)entityEntry.Entity).Created = DateTime.Now;
                }
            }
        }

        private void ApplyGlobalConfigurations(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder
                        .Entity(entityType.ClrType)
                        .Property(nameof(IAuditableEntity.Created))
                        .IsRequired()
                        .HasDefaultValueSql("GETDATE()");

                    modelBuilder
                        .Entity(entityType.ClrType)
                        .Property(nameof(IAuditableEntity.Modified))
                        .IsRequired()
                        .HasDefaultValueSql("GETDATE()");
                }
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
    }
}
