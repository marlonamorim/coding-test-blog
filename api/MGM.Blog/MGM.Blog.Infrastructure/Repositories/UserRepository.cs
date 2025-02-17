using MGM.Blog.Domain.Models;
using MGM.Blog.Domain.Repositories;
using MGM.Blog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MGM.Blog.Infrastructure.Repositories
{
    internal class UserRepository(BlogDbContext blogDbContext) : IUserRepository
    {
        public async Task CreateAsync(User entity)
        {
            await blogDbContext.Users.AddAsync(entity);
            await blogDbContext.SaveChangesAsync();
        }

        public async Task<bool> CredentialIsValidAsync(string email, string password)
            => await blogDbContext.Users.AnyAsync(x => x.Email.Equals(email) && x.Password.Equals(password));

        public async Task<User?> GetByEmailAsync(string email)
            => await blogDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }
}
