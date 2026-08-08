using BuzzUp_API.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BuzzUp_API.DataAccess
{
    public class BuzzUpContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<UserUseCase> UserUseCases { get; set; }
        public DbSet<UseCaseLog> UseCaseLogs { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<PasswordResetToken> PasswordResetTokens { get; set; }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reaction> Reactions { get; set; }
        public DbSet<UserFriendship> UserFriendships { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<VisibilityType> VisibilityTypes { get; set; }
        public DbSet<FeelingType> FeelingTypes { get; set; }
        public DbSet<ReactionType> ReactionTypes { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<FriendRequestStatus> FriendRequestStatuses { get; set; }
        public DbSet<PostMedia> PostMedias { get; set; }
        public DbSet<PostMediaType> PostMediaTypes { get; set; }
        public DbSet<PostTag> PostTags { get; set; }
        public DbSet<SavedPost> SavedPosts { get; set; }
        public DbSet<Country> Countries { get; set; }


        private readonly string _connectionString;

        public BuzzUpContext(string connectionString)
        {
            this._connectionString = connectionString;
        }
        public BuzzUpContext()
        {
            this._connectionString = "Data Source=.;Initial Catalog=BuzzUp;TrustServerCertificate=true;Integrated security = true;";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString).UseLazyLoadingProxies();

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);

            modelBuilder.Entity<UserUseCase>().HasKey(x => new
            {
                x.UserId,
                x.UseCaseId
            });

            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "Admin",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                },
                new Role
                {
                    Id = 2,
                    Name = "User",
                    IsActive = true,
                    CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
                }
            );

            modelBuilder.Entity<Country>().ToTable("Countries");

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "Austria" },
                new Country { Id = 2, Name = "Belgium" },
                new Country { Id = 3, Name = "Bosnia and Herzegovina" },
                new Country { Id = 4, Name = "Bulgaria" },
                new Country { Id = 5, Name = "Canada" },
                new Country { Id = 6, Name = "China" },
                new Country { Id = 7, Name = "Croatia" },
                new Country { Id = 8, Name = "Czech Republic" },
                new Country { Id = 9, Name = "Denmark" },
                new Country { Id = 10, Name = "Egypt" },
                new Country { Id = 11, Name = "Finland" },
                new Country { Id = 12, Name = "France" },
                new Country { Id = 13, Name = "Germany" },
                new Country { Id = 14, Name = "Greece" },
                new Country { Id = 15, Name = "Hungary" },
                new Country { Id = 16, Name = "India" },
                new Country { Id = 17, Name = "Indonesia" },
                new Country { Id = 18, Name = "Ireland" },
                new Country { Id = 19, Name = "Italy" },
                new Country { Id = 20, Name = "Japan" },
                new Country { Id = 21, Name = "Mexico" },
                new Country { Id = 22, Name = "Montenegro" },
                new Country { Id = 23, Name = "Netherlands" },
                new Country { Id = 24, Name = "Norway" },
                new Country { Id = 25, Name = "Poland" },
                new Country { Id = 26, Name = "Portugal" },
                new Country { Id = 27, Name = "Romania" },
                new Country { Id = 28, Name = "Russia" },
                new Country { Id = 29, Name = "Serbia" },
                new Country { Id = 30, Name = "Slovakia" },
                new Country { Id = 31, Name = "Slovenia" },
                new Country { Id = 32, Name = "Spain" },
                new Country { Id = 33, Name = "Sweden" },
                new Country { Id = 34, Name = "Switzerland" },
                new Country { Id = 35, Name = "Turkey" },
                new Country { Id = 36, Name = "Ukraine" },
                new Country { Id = 37, Name = "United Arab Emirates" },
                new Country { Id = 38, Name = "United Kingdom" },
                new Country { Id = 39, Name = "United States" },
                new Country { Id = 40, Name = "Brazil" },
                new Country { Id = 41, Name = "Argentina" },
                new Country { Id = 42, Name = "South Africa" },
                new Country { Id = 43, Name = "Nigeria" },
                new Country { Id = 44, Name = "Saudi Arabia" },
                new Country { Id = 45, Name = "South Korea" },
                new Country { Id = 46, Name = "Thailand" },
                new Country { Id = 47, Name = "Vietnam" },
                new Country { Id = 48, Name = "Philippines" },
                new Country { Id = 49, Name = "Pakistan" },
                new Country { Id = 50, Name = "Bangladesh" }
            );

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
