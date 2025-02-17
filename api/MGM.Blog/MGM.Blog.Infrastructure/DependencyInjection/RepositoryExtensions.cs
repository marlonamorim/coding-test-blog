using MGM.Blog.Domain.Repositories;
using MGM.Blog.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MGM.Blog.Infrastructure.DependencyInjection
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPostRepository, PostRepository>();
            services.AddSingleton<IUserRepository, UserRepository>();

            return services;
        }
    }
}
