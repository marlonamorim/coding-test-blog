using MGM.Blog.Infrastructure.Api.ErrorHandling;
using MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MGM.Blog.Infrastructure.Api.Filters
{
    public class ExceptionFilter(IActionResultErrorBuilder actionResultErrorBuilder) : IExceptionFilter
    {
        private readonly IActionResultErrorBuilder _actionResultErrorBuilder = actionResultErrorBuilder;

        public void OnException(ExceptionContext context)
        {

            context.Result = context.Exception switch
            {
                HttpClientUnauthorizedException httpClientUnauthorizedException => _actionResultErrorBuilder.HttpClientUnauthorizedAccess(httpClientUnauthorizedException),
                UnauthorizedAccessException unauthorizedAccessException => _actionResultErrorBuilder.HttpClientUnauthorizedAccess(unauthorizedAccessException),
                HttpClientBadRequestException badRequestException => _actionResultErrorBuilder.HttpClientBadRequestException(badRequestException),
                HttpClientResourceNotFoundException resourceNotFoundException => _actionResultErrorBuilder.HttpClientResourceNotFoundException(resourceNotFoundException),
                _ => _actionResultErrorBuilder.UnknowException(context.Exception)
            };
        }
    }
}
