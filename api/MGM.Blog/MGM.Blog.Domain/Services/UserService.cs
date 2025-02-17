using MGM.Blog.Domain.Converters;
using MGM.Blog.Domain.Dtos;
using MGM.Blog.Domain.Repositories;

namespace MGM.Blog.Domain.Services
{
    internal class UserService(IUserRepository repository) : IUserService
    {
        public async Task CreateAsync(UserDto dto) =>
            await repository.CreateAsync(dto.ToEntity());
    }
}
