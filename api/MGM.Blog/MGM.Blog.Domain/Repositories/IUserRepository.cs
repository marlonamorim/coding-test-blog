using MGM.Blog.Domain.Models;

namespace MGM.Blog.Domain.Repositories
{
    public interface IUserRepository
    {
        Task CreateAsync(User entity);
        Task<bool> CredentialIsValidAsync(string email, string password);
        Task<User?> GetByEmailAsync(string email);
    }
}
