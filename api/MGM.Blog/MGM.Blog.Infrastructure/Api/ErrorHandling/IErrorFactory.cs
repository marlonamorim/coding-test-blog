using Microsoft.AspNetCore.Mvc;

namespace MGM.Blog.Infrastructure.Api.ErrorHandling
{
    public interface IErrorFactory
    {
        ProblemDetails CreateUnauthorized(string detail);
        ProblemDetails CreateNotFound(string detail);
        ProblemDetails CreateBadRequest(string detail);
        ProblemDetails CreateInternalServerError(string detail);
    }

    public class ErrorFactory : IErrorFactory
    {
        private const string UNAUTHORIZED = "Unauthorized";
        private const string NOT_FOUND = "NotFound";
        private const string BAD_REQUEST = "BadRequest";
        private const string INTERNAL_SERVER_ERROR = "InternalServerError";

        public ProblemDetails CreateBadRequest(string detail)
        {
            var problemDetails = new ProblemDetails
            {
                Title = BAD_REQUEST,
                Detail = detail
            };

            return problemDetails;
        }

        public ProblemDetails CreateInternalServerError(string detail)
        {
            var problemDetails = new ProblemDetails
            {
                Title = INTERNAL_SERVER_ERROR,
                Detail = detail
            };

            return problemDetails;
        }

        public ProblemDetails CreateNotFound(string detail)
        {
            var problemDetails = new ProblemDetails
            {
                Title = NOT_FOUND,
                Detail = detail
            };

            return problemDetails;
        }

        public ProblemDetails CreateUnauthorized(string detail)
        {
            var problemDetails = new ProblemDetails
            {
                Title = UNAUTHORIZED,
                Detail = detail
            };

            return problemDetails;
        }
    }
}
