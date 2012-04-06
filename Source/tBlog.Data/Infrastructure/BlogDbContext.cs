using System.Data.Entity;
using tBlog.Domain.Entities;

namespace tBlog.Data.Infrastructure
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Entry> Entries { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public class EfDbContextInitializer : DropCreateDatabaseIfModelChanges<BlogDbContext>
    {
    }
}
