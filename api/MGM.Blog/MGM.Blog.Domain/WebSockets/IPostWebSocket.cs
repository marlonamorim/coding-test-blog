using Microsoft.AspNetCore.Http;

namespace MGM.Blog.Domain.WebSockets
{
    public interface IPostWebSocket
    {
        Task SendAsync(HttpContext context);
    }
}
