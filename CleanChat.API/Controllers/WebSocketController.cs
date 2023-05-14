using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CleanChat.API.Controllers
{
    public class WebSocketController : ControllerBase
    {
        [HttpGet]
        [Route("api/websocket")]
        public async Task<IActionResult> Connect()
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                return BadRequest();
            }

            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            // Start a new thread to handle WebSocket messages
            Task.Run(() => ProcessWebSocketMessages(webSocket));

            // Return an empty response to signal that the WebSocket connection was successfully established
            return new EmptyResult();
        }

        private async Task ProcessWebSocketMessages(WebSocket webSocket)
        {
            var buffer = new byte[1024];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                    // Do something with the message received from the WebSocket
                }
            }
        }
    }
}
