using MGM.Blog.AppServices.ViewModel;
using MGM.Blog.Domain.Dtos;

namespace MGM.Blog.AppServices.Services
{
    public interface IPostAppService
    {
        Task CreateAsync(PostViewModel vm);

        Task<IEnumerable<PostDto>> ListByUserAsync();

        Task DeleteAsync(Guid id);

        Task UpdateAsync(PostViewModel vm);
    }
}
