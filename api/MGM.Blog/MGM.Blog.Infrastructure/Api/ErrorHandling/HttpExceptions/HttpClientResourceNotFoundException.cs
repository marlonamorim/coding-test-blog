using System.Net;

namespace MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions
{
    public class HttpClientResourceNotFoundException(string message) : HttpClientRequestException(message, HttpStatusCode.NotFound)
    {
    }
}
