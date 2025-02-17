using MGM.Blog.Domain.Models;

namespace MGM.Blog.Domain.Repositories
{
    public interface IPostRepository
    {
        Task CreateAsync(Post entity);

        Task<IEnumerable<Post>> ListByUserIdAsync(Guid userId);

        Task<Post?> GetByIdAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<int?> UpdateAsync(Guid id, Post entity);
    }
}
