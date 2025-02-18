using MGM.Blog.Domain.Services;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace MGM.Blog.Domain.WebSockets
{
    internal class PostWebSocket(IPostService postService) : IPostWebSocket
    {
        public async Task SendAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

            var referenceDate = DateTime.UtcNow;
            while (true)
            {
                var posts = await postService.ListNewsAsync(referenceDate);
                foreach (var post in posts)
                {
                    var data = Encoding.ASCII.GetBytes(JsonConvert.SerializeObject(post));
                    await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
                }
                referenceDate = DateTime.UtcNow;
                await Task.Delay(5000);
            }
        }
    }
}
