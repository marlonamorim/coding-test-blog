using Microsoft.AspNetCore.Http;

namespace MGM.Blog.Domain.Services
{
    internal class ContextAccessorService(IHttpContextAccessor contextAccessor) : IContextAccessorService
    {
        private const string CLAIM_TYPE_NAME_IDENTIFIER = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";

        private readonly IHttpContextAccessor _contextAccessor = contextAccessor;

        public string? GetUserId()
        {
            var context = _contextAccessor.HttpContext;

            return context?.User?.Claims?.FirstOrDefault(c => c.Type == CLAIM_TYPE_NAME_IDENTIFIER)?.Value;
        }
    }
}
