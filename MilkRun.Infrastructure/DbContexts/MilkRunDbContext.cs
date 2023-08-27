using Microsoft.EntityFrameworkCore;
using MilkRun.ApplicationCore.Models;

namespace MilkRun.Infrastructure.DbContexts
{
    public class MilkRunDbContext: DbContext
    {
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }

        public MilkRunDbContext(DbContextOptions<MilkRunDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MilkRunDbContext).Assembly);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            optionsBuilder.EnableSensitiveDataLogging();
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is Entity && (
                                       e.State == EntityState.Added
                                                              || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((Entity)entityEntry.Entity).ModifiedOn = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((Entity)entityEntry.Entity).CreatedOn = DateTime.UtcNow;
                }
            }

            return base.SaveChanges();
        }
    }
}
