using MGM.Blog.Domain.WebSockets;
using Microsoft.AspNetCore.Mvc;

namespace MGM.Blog.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "WebSockets")]
    public class WebSocketController(IPostWebSocket postWebSocket) : ControllerBase
    {
        [HttpGet("/ws")]
        public async Task GetAsync() => 
            await postWebSocket.SendAsync(HttpContext);
    }
}
