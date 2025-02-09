using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuzzUp_API.DataAccess
{
    public class BuzzUpContext : DbContext
    {
        private readonly string _connectionString;

        public BuzzUpContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public BuzzUpContext()
        {
            this._connectionString = "Data Source=DESKTOP-CG6HMPT\\SQLEXPRESS;Initial Catalog=BuzzUp;TrustServerCertificate=true;Integrated security = true;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString).UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            //modelBuilder.Entity<UserUseCase>().HasKey(x => new
            //{
            //    x.UserId,
            //    x.UseCaseId
            //});

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            IEnumerable<EntityEntry> entries = this.ChangeTracker.Entries();

            foreach (EntityEntry entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.IsActive = true;
                        e.CreatedAt = DateTime.UtcNow;
                    }
                }

                if (entry.State == EntityState.Modified)
                {
                    if (entry.Entity is Entity e)
                    {
                        e.UpdatedAt = DateTime.UtcNow;
                    }
                }
            }

            return base.SaveChanges();
        }
    }
}
