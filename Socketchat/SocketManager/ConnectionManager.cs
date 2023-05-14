using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace Socketchat.SocketManager
{
    public class ConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> _connection = new ConcurrentDictionary<string, WebSocket>();

        public ConnectionManager() 
        {
            
        }  

        public WebSocket GetSocketById(string name)
        {
            return _connection.FirstOrDefault(s => s.Key == name).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAllConnections() 
        {
            return _connection;
        }

        public string GetId(WebSocket socket)
        {
            return _connection.FirstOrDefault(c => c.Value == socket).Key;
        }

        public async Task RemoveWebSocketAsync(string name)
        {
            _connection.TryRemove(name, out WebSocket socket);
            await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "socket connection closed",CancellationToken.None);
        }

        public void AddSocket(WebSocket socket)
        {
            _connection.TryAdd(GenerateRandomString(), socket);
        }

        public static string GenerateRandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            var result = new string(Enumerable.Repeat(chars, 7)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            return result;
        }
    }

    
}
