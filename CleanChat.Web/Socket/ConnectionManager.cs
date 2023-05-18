using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace CleanChat.Web.Socket
{
    // class manage all sockets and topic's sockets (add, remove, get)
    public class ConnectionManager
    {
        //manege Topic and sockets that subcribed the topic
        private Dictionary<string, List<WebSocket>> _Topicsockets = new Dictionary<string, List<WebSocket>>();
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();
        //get list socket by TopicId
        public List<WebSocket> GetListSocketById(string topic)
        {
            if (_Topicsockets.ContainsKey(topic))
            {
                return _Topicsockets.FirstOrDefault(p => p.Key == topic).Value;
            }
            else
            {
                return new List<WebSocket>();
            }
        }
        //get socket by socketID in dictionary _sockets
        public WebSocket GetSocketById(string id)
        {
            return _sockets.FirstOrDefault(p => p.Key == id).Value;
        }
        //get SocketId by socket
        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        //Get all socket and socketId avaiable
        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return _sockets;
        }
        //Get topic based on socket.
        public string GetTopic(WebSocket socket)
        {
            return _Topicsockets.FirstOrDefault(p => p.Value.Contains(socket)).Key;
        }
        //subcribe socket to topic.
        public void SubcribeSocket(WebSocket socket, string topic)
        {
            WebSocket socketValue;
            _sockets.TryRemove(GetId(socket), out socketValue);
            //check if topic already existed then add socket 
            if (_Topicsockets.ContainsKey(topic))
            {
                _Topicsockets[topic].Add(socketValue);
            }
            //else create new topic then add socket
            else
            {
                _Topicsockets.Add(topic, new List<WebSocket>());
                _Topicsockets[topic].Add(socketValue);
            }
        }
        //add socket with random Id to list socket _socket
        public void AddSocket(WebSocket socket)
        {
            _sockets.TryAdd(CreateConnectionId(), socket);
        }
        //Get all available topics
        public List<string> GetListTopic()
        {
            List<string> result = new List<string>();
            foreach (var item in _sockets)
            {
                result.Add(item.Key);
            }
            return result;
        }
        //remove socket from collection TopicSocket
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

            // after remove socket, close thi connection with socket
            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the ConnectionManager",
                                    cancellationToken: CancellationToken.None);
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
