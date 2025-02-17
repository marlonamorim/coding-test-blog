using MGM.Blog.Domain.Dtos;

namespace MGM.Blog.Domain.Services
{
    public interface IPostService
    {
        Task CreateAsync(PostDto dto);

        Task<IEnumerable<PostDto>> ListByUserAsync();

        Task DeleteAsync(Guid id);

        Task<ResultDto> UpdateAsync(Guid id, PostDto dto);
    }
}
