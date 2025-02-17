using MGM.Blog.AppServices.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MGM.Blog.AppServices.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddSingleton<IPostAppService, PostAppService>();
            services.AddSingleton<IUserAppService, UserAppService>();

            return services;
        }
    }
}
