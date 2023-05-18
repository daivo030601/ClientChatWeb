using System.Net.WebSockets;
using System.Text;

namespace CleanChat.Web.Socket
{
    //handle all connect and disconnect event from socket, handle send and receive
    public abstract class WebSocketHandler
    {
        //using class WebSocketConnectionManager to handle all event with socket
        protected ConnectionManager WebSocketConnectionManager { get; set; }

        public WebSocketHandler(ConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }

        public virtual async Task OnConnected(WebSocket socket)
        {
            WebSocketConnectionManager.AddSocket(socket);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await WebSocketConnectionManager.RemoveSocket(socket);
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            //check ensures that the message is only sent when the connection is open and ready.
            if (socket.State != WebSocketState.Open)
                return;

            await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                    offset: 0,
                                                                    count: message.Length),
                                    messageType: WebSocketMessageType.Text,
                                    endOfMessage: true, //make sure this is the last one in the sequence
                                    cancellationToken: CancellationToken.None);//no cancellation is requested.
        }


        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);
        }
        public async Task SubcribeSocket(string topic)
        {
            foreach (var pair in WebSocketConnectionManager.GetAll())
            {
                if (pair.Value.State == WebSocketState.Open)
                    WebSocketConnectionManager.SubcribeSocket(pair.Value, topic);
            }
        }
        //split message with "," to get topic part and message part
        public async Task SendMessageToAllAsync(string message)
        {
            string[] msgParts = message.Split(",");
            foreach (var socket in WebSocketConnectionManager.GetListSocketById(msgParts[0]))
            {
                if (socket.State == WebSocketState.Open)
                    await SendMessageAsync(socket, msgParts[1]);
            }
        }
        //asbtract to use custom code for OnConnected and OnDisconnected methods
        public abstract Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer);
    }
}
