namespace MGM.Blog.Domain.Dtos
{
    public record PostDto(Guid Id, string Text, string Title, Guid? UserId = null);
}
