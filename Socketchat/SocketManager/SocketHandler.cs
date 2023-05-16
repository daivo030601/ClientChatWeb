using System.Net.WebSockets;
using System.Reflection;
using System.Text;

namespace Socketchat.SocketManager
{
    public abstract class SocketHandler
    {
        public ConnectionManager _connections;

        public SocketHandler(ConnectionManager connections)
        {
            _connections = connections;
        }

        public virtual async Task OnConnected(WebSocket webSocket)
        {
            Task.Run(() => _connections.AddSocket(webSocket));
        }

        public virtual async Task OnDisconnected(WebSocket webSocket)
        {
            await _connections.RemoveWebSocketAsync(_connections.GetId(webSocket));
        }

        public async Task SendMessage(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;
            await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message),0,message.Length),WebSocketMessageType.Text,true, CancellationToken.None);

        }

        public async Task SendMessage(string name, string message)
        {
            await SendMessage(_connections.GetSocketById(name),message);
        }

        public async Task SendMessageToAll(string message)
        {
            foreach (var con in _connections.GetAllConnections())
            {
                SendMessage(con.Value,message);
            }
        }

        public abstract Task Receive(WebSocket webSocket, WebSocketReceiveResult result, byte[] buffer);
    }

    
}
