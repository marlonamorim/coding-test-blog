using MGM.Blog.Domain.ValueObjects;

namespace MGM.Blog.Domain.Dtos
{
    public record UserDto(Guid Id, string FirstName, string LastName, DateTime BirthDate, string Email, string Password, Cpf TaxDocument, IEnumerable<PostDto>? Posts = null);
}
