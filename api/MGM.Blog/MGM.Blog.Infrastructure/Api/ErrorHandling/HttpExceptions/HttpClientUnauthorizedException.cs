using System.Net;

namespace MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions
{
    public class HttpClientUnauthorizedException(string message) : HttpClientRequestException(message, HttpStatusCode.Unauthorized)
    {
    }
}
