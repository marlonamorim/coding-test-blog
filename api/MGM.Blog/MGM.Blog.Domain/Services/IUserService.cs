using MGM.Blog.Domain.Dtos;

namespace MGM.Blog.Domain.Services
{
    public interface IUserService
    {
        Task CreateAsync(UserDto dto);
    }
}
