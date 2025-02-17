using MGM.Blog.Infrastructure.Api.ErrorHandling;
using MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions;
using MGM.Blog.Infrastructure.Api.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace MGM.Blog.Infrastructure.DependencyInjection
{
    public static class ErrorHandlingServiceExtensions
    {
        public static IServiceCollection AddErrorHandlerServices(this IServiceCollection services)
        {
            services.AddSingleton<IErrorFactory, ErrorFactory>();
            services.AddSingleton<IActionResultErrorBuilder, ActionResultErrorBuilder>();
            services.AddSingleton<IDefaultHttpClientErrorResponseHandler, DefaultHttpClientErrorResponseHandler>();
            services.AddSingleton<ExceptionFilter>();

            return services;
        }
    }
}
