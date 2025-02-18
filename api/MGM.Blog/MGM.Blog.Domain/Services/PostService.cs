using MGM.Blog.Domain.Converters;
using MGM.Blog.Domain.Dtos;
using MGM.Blog.Domain.Repositories;

namespace MGM.Blog.Domain.Services
{
    internal class PostService(IPostRepository repository, IContextAccessorService contextAccessorService) : IPostService
    {
        public async Task CreateAsync(PostDto dto)
            => await repository.CreateAsync(dto.ToEntity());

        public async Task DeleteAsync(Guid id)
            => await repository.DeleteAsync(id);

        public async Task<IEnumerable<PostDto>> ListAsync()
        {
            var posts = await repository.ListAsync();
            return posts
                .OrderByDescending(x => x.Created)
                .ToCollectionPostDto();
        }

        public async Task<IEnumerable<PostDto>> ListNewsAsync(DateTime referenceDate)
        {
            var posts = await repository.ListNewsAsync(referenceDate);
            return posts
                .OrderByDescending(x => x.Created)
                .ToCollectionPostDto();
        }

        public async Task<ResultDto> UpdateAsync(Guid id, PostDto dto)
        {
            var postToUpdate = await repository.GetByIdAsync(id);
            if (postToUpdate is null)
                return new ResultDto { Success = false, Error = ["Invalid Post Id"] };

            var rawUserId = contextAccessorService.GetUserId();
            _ = Guid.TryParse(rawUserId, out Guid userId);
            if (!postToUpdate.UserId.Equals(userId))
                return new ResultDto { Success = false, Error = ["Post Id does not match logged in user"] };

            var result = await repository.UpdateAsync(id, dto.ToEntity());
            if (result is null)
                return new ResultDto { Success = false, Error = ["Invalid Post Id"] };

            return new ResultDto();
        }
    }
}
