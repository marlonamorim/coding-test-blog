using MGM.Blog.Domain.Dtos;

namespace MGM.Blog.Domain.Services
{
    public interface IPostService
    {
        Task CreateAsync(PostDto dto);

        Task<IEnumerable<PostDto>> ListAsync();

        Task<IEnumerable<PostDto>> ListNewsAsync(DateTime referenceDate);

        Task DeleteAsync(Guid id);

        Task<ResultDto> UpdateAsync(Guid id, PostDto dto);
    }
}
