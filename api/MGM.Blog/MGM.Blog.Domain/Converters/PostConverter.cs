using MGM.Blog.Domain.Dtos;
using MGM.Blog.Domain.Models;

namespace MGM.Blog.Domain.Converters
{
    public static class PostConverter
    {
        public static Post ToEntity(this PostDto dto)
            => Post.Create(dto.Text, dto.Title, dto.UserId!.Value);

        public static PostDto ToDto(this Post entity)
            => new(entity.Id, entity.Text, entity.Title, entity.UserId);

        public static IEnumerable<Post> ToCollectionPostEntity(this IEnumerable<PostDto> posts)
            => posts.Select(x => Post.Create(x.Text, x.Title, x.UserId!.Value));

        public static IEnumerable<PostDto> ToCollectionPostDto(this IEnumerable<Post> posts)
            => posts.Select(x => new PostDto(x.Id, x.Text, x.Title, x.UserId));
    }
}
