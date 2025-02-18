using MGM.Blog.Domain.Models;
using MGM.Blog.Domain.Repositories;
using MGM.Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MGM.Blog.Infrastructure.Repositories
{
    internal class PostRepository(BlogDbContext blogDbContext) : IPostRepository
    {
        public async Task CreateAsync(Post entity)
        {
            await blogDbContext.Posts.AddAsync(entity);
            await blogDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var postToDelete = await blogDbContext.Posts.FindAsync(id);
            if (postToDelete != null)
            {
                blogDbContext.Posts.Remove(postToDelete);
                await blogDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Post>> ListAsync()
            => await blogDbContext.Posts.ToListAsync();

        public async Task<IEnumerable<Post>> ListNewsAsync(DateTime referenceDate)
            => await blogDbContext.Posts.Where(x => x.Created.UtcDateTime >= referenceDate).ToListAsync();

        public async Task<Post?> GetByIdAsync(Guid id)
            => await blogDbContext.Posts.FirstOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<int?> UpdateAsync(Guid id, Post entity)
        {
            var postToUpdate = await blogDbContext.Posts.FindAsync(id);
            if (postToUpdate is null) return default;

            postToUpdate.Update(entity.Text, entity.Title, entity.UserId);
            return await blogDbContext.SaveChangesAsync();
        }
    }
}
