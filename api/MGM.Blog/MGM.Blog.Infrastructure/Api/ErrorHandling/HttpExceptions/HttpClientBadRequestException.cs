using System.Net;

namespace MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions
{
    public class HttpClientBadRequestException(string message) : HttpClientRequestException(message, HttpStatusCode.BadRequest)
    {
    }
}
