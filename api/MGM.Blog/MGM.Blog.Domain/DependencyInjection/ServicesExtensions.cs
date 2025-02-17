using MGM.Blog.Domain.Services;
using MGM.Blog.Domain.WebSockets;
using Microsoft.Extensions.DependencyInjection;

namespace MGM.Blog.Domain.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddSingleton<IContextAccessorService, ContextAccessorService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<IPostService, PostService>();
            services.AddSingleton<IPostWebSocket, PostWebSocket>();

            return services;
        }
    }
}
