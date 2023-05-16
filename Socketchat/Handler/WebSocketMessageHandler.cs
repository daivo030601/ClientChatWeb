using Socketchat.SocketManager;
using System.Net.WebSockets;
using System.Text;

namespace Socketchat.Handler
{
    public class WebSocketMessageHandler : SocketHandler
    {
        public WebSocketMessageHandler(ConnectionManager connections) : base(connections)
        {
        }

        public override async Task OnConnected(WebSocket webSocket)
        {
            await base.OnConnected(webSocket);
            var socketId = _connections.GetId(webSocket);
            await SendMessageToAll($"{socketId} just join the party *****");

        }

        public override async Task Receive(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = _connections.GetId(webSocket);
            var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer,0,result.Count)}";

            await SendMessageToAll(message);
        }
    }
}
