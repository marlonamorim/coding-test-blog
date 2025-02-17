using MGM.Blog.Domain.Dtos;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
using System.Text;

namespace MGM.Blog.Domain.WebSockets
{
    internal class PostWebSocket : IPostWebSocket
    {
        public async Task SendAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();

            while (true)
            {
                var data = Encoding.ASCII.GetBytes($"Nova postagem: {JsonConvert.SerializeObject(new PostDto(Guid.NewGuid(), "Lorem Ipsum", "Nova Postagem"))}");
                await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
                await Task.Delay(5000);
            }
        }
    }
}
