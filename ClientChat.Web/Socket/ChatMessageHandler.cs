﻿using System.Net.WebSockets;
using System.Text;

namespace CleanChat.Web.Socket
{
    public class ChatMessageHandler : WebSocketHandler
    {
        public ChatMessageHandler(ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);
            //await SendMessageToAllAsync($"{socketId}, is now connected");
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);
            var message = $"{Encoding.UTF8.GetString(buffer, 0, result.Count)}";

            string[] msgParts = message.Split("-");
            if (msgParts[0] == "SUBCRIBE")
            {
                await SubcribeSocket(msgParts[1]);
            }
            else if (msgParts[0] == "MESSAGE")
            {
                await SendMessageToAllAsync(msgParts[1]);
            }
        }
    }
}
