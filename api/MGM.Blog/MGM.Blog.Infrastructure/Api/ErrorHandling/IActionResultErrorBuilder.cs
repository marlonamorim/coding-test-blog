using MGM.Blog.Infrastructure.Api.ErrorHandling.HttpExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MGM.Blog.Infrastructure.Api.ErrorHandling
{
    public interface IActionResultErrorBuilder
    {
        IActionResult HttpClientUnauthorizedAccess(HttpClientUnauthorizedException exception);

        IActionResult HttpClientUnauthorizedAccess(UnauthorizedAccessException exception);

        IActionResult HttpClientResourceNotFoundException(HttpClientResourceNotFoundException exception);

        IActionResult HttpClientBadRequestException(HttpClientBadRequestException exception);

        IActionResult UnknowException(Exception exception);
    }

    public class ActionResultErrorBuilder(IErrorFactory errorFactory) : IActionResultErrorBuilder
    {
        private readonly IErrorFactory _errorFactory = errorFactory;

        public IActionResult HttpClientBadRequestException(HttpClientBadRequestException exception)
        {
            return new BadRequestObjectResult(_errorFactory.CreateBadRequest(exception.Message));
        }

        public IActionResult HttpClientResourceNotFoundException(HttpClientResourceNotFoundException exception)
        {
            return new NotFoundObjectResult(_errorFactory.CreateNotFound(exception.Message));
        }

        public IActionResult HttpClientUnauthorizedAccess(UnauthorizedAccessException exception)
        {
            return new ObjectResult(_errorFactory.CreateUnauthorized(exception.Message)) 
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        public IActionResult HttpClientUnauthorizedAccess(HttpClientUnauthorizedException exception)
        {
            return new ObjectResult(_errorFactory.CreateUnauthorized(exception.Message))
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }


        public IActionResult UnknowException(Exception exception)
        {
            return new ObjectResult(_errorFactory.CreateInternalServerError($"Unexpected error. {exception.Message}"))
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
    }
}
