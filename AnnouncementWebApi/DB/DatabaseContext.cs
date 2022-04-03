using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AnnouncementWebApi.DB
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Announcement> Announcements { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename = AnnouncementsDatabase.db",
                options =>
                {
                    options.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
                });
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Announcement>().ToTable("Announcements", "test");
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.HasIndex(a => a.Title).IsUnique();
                entity.Property(a => a.Description);
                entity.Property(a => a.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(a => a.EditDate).HasDefaultValueSql("null");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
