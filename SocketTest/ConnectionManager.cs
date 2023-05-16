using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace SocketTest
{
    public class ConnectionManager
    {

        private Dictionary<string, List<WebSocket>> _Topicsockets = new Dictionary<string, List<WebSocket>>();
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public List<WebSocket> GetListSocketById(string topic)
        {
            if (_Topicsockets.ContainsKey(topic))
            {
                return _Topicsockets.FirstOrDefault(p => p.Key == topic).Value;
            } else
            {
                return new List<WebSocket>();
            }
        }

        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }

        public string GetTopic(WebSocket socket)
        {
            return _Topicsockets.FirstOrDefault(p => p.Value.Contains(socket)).Key;
        }
        public void SubcribeSocket(WebSocket socket, string topic)
        {
            WebSocket socketValue;
            _sockets.TryRemove(GetId(socket), out socketValue);
            if (_Topicsockets.ContainsKey(topic))
            {
                _Topicsockets[topic].Add(socketValue);
            }
            else
            {
                _Topicsockets.Add(topic, new List<WebSocket>());
                _Topicsockets[topic].Add(socketValue);
            }
        }

        public void AddSocket(WebSocket socket)
        {
            _sockets.TryAdd(CreateConnectionId(), socket);
        }

        public List<string> GetListTopic()
        {
            List<string> result = new List<string>();
            foreach (var item in _sockets)
            {
                result.Add(item.Key);
            }
            return result;
        }

        public async Task RemoveSocket(WebSocket socket)
        {
            var listTopics = GetListTopic();
            foreach (var topic in listTopics)
            {
                if (_Topicsockets[topic].Contains(socket))
                {
                    _Topicsockets[topic].Remove(socket);
                }
            }

            WebSocket socketValue;
            _sockets.TryRemove(GetId(socket), out socketValue);

            await socketValue.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the ConnectionManager",
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
