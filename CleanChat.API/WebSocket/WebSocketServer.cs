using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System;

namespace CleanChat.API.WebSocketServer
{
    public class WebSocketServer
    {
        private const int PORT = 8080;
        private TcpListener _listener;
        private Thread _serverThread;
        public void Start() { 
            _listener = new TcpListener(IPAddress.Any, PORT);
            _listener.Start();
            _serverThread = new Thread(ListenLoop);
            _serverThread.Start();
        }

        private void ListenLoop() { 
            while (true) 
            { 
                TcpClient client = _listener.AcceptTcpClient(); 
                HandleClient(client); 
            }
        }

        private void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();

            // Read the HTTP request from the client
            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);

            // Extract the WebSocket key from the request
            string key = "";
            foreach (string line in request.Split('\n'))
            {
                if (line.StartsWith("Sec-WebSocket-Key:"))
                {
                    key = line.Substring(18).Trim();
                    break;
                }
            }

            // Compute the WebSocket response
            string response = "HTTP/1.1 101 Switching Protocols\r\n";
            response += "Upgrade: websocket\r\n";
            response += "Connection: Upgrade\r\n";
            response += "Sec-WebSocket-Accept: " + ComputeWebSocketAccept(key) + "\r\n\r\n";

            // Send the WebSocket response to the client
            byte[] responseBytes = Encoding.UTF8.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);

            // Start reading WebSocket frames from the client
            while (client.Connected)
            {
                buffer = new byte[1024];
                bytesRead = stream.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0)
                {
                    // Parse the WebSocket frame and handle the message
                    WebSocketFrame frame = WebSocketFrame.Parse(buffer, bytesRead);
                    //if (frame.Opcode == WebSocketOpcode.Text)
                    //{
                    //    string message = Encoding.UTF8.GetString(frame.Payload);
                    //    Console.WriteLine("Received message: " + message);
                    //}
                }
            }
        }
        private string ComputeWebSocketAccept(string key)
        {
            const string guid = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";
            string concatenated = key + guid;
            byte[] sha1 = System.Security.Cryptography.SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(concatenated));
            return Convert.ToBase64String(sha1);
        }

        private class WebSocketFrame
        {
            public byte Opcode { get; private set; }
            public byte[] Payload { get; private set; }

            public static WebSocketFrame Parse(byte[] buffer, int length)
            {
                WebSocketFrame frame = new WebSocketFrame();
                frame.Opcode = (byte)(buffer[0] & 0x0F);
                int payloadLength = buffer[1] & 0x7F;
                int maskIndex = 2;
                if (payloadLength == 126)
                {
                    payloadLength = (buffer[2] << 8) | buffer[3];
                    maskIndex = 4;
                }
                else if (payloadLength == 127)
                {
                    payloadLength = (buffer[2] << 56) | (buffer[3] << 48) | (buffer[4] << 40) | (buffer[5] << 32) | (buffer[6] << 24) | (buffer[7] << 16) | (buffer[8] << 8) | buffer[9];
                    maskIndex = 10;
                }
                if ((buffer[1] & 0x80) == 0x80)
                {
                    byte[] mask = new byte[] { buffer[maskIndex], buffer[maskIndex + 1], buffer[maskIndex + 2], buffer[maskIndex + 3] };
                    frame.Payload = new byte[payloadLength];
                    for (int i = 0; i < payloadLength; i++)
                    {
                        frame.Payload[i] = (byte)(buffer[maskIndex + 4 + i] ^ mask[i % 4]);
                    }
                }
                else
                {
                    frame.Payload = new byte[payloadLength];
                    Array.Copy(buffer, maskIndex, frame.Payload, 0, payloadLength);
                }
                return frame;
            }
        }
    }
}
