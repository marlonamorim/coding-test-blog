using System.Net;

namespace MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions
{
    public interface IDefaultHttpClientErrorResponseHandler
    {
        HttpClientRequestException ThrowResponseError(HttpStatusCode httpStatusCode, string requestUri, string messageResponse);
    }

    public class DefaultHttpClientErrorResponseHandler : IDefaultHttpClientErrorResponseHandler
    {
        private const string UNKNOWN_ERROR_TEMPLATE = "Fail on calling the API for endpoint {0}.";
        public HttpClientRequestException ThrowResponseError(HttpStatusCode httpStatusCode, string requestUri, string responseBody)
        {
            return httpStatusCode switch
            {
                HttpStatusCode.NotFound => new HttpClientResourceNotFoundException(responseBody),
                HttpStatusCode.BadRequest => new HttpClientBadRequestException(responseBody),
                HttpStatusCode.Unauthorized => new HttpClientUnauthorizedException(responseBody),
                _ => new HttpClientRequestException(string.Format(UNKNOWN_ERROR_TEMPLATE, requestUri), HttpStatusCode.InternalServerError),
            };
        }
    }
}
