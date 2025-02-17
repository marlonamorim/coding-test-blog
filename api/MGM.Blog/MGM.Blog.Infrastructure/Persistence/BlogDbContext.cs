using MGM.Blog.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace MGM.Blog.Infrastructure.Persistence
{
    public class BlogDbContext(DbContextOptions<BlogDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Post> Posts => Set<Post>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("blog");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
