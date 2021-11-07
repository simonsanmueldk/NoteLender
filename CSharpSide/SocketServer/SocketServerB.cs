using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketServer
{
    class SocketServerB
    {
        public static string data = null;

        static void Main(string[] args)
        {
            IPHostEntry host = Dns.GetHostEntry("127.0.0.1");  
            IPAddress ipAddress = host.AddressList[0];  
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2910);
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 2910); 
            
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(serverAddress);  
            serverSocket.Listen(10);
            Console.WriteLine("Server started..");
            // accept client
            Socket socketToClient = serverSocket.Accept();
            Console.WriteLine("Client connected..");
            
            // read from client
            byte[] rcvLenBytes = new byte[4];
            socketToClient.Receive(rcvLenBytes);
            int rcvLen = BitConverter.ToInt32(rcvLenBytes, 0);
            byte[] rcvBytes = new byte[rcvLen];
            socketToClient.Receive(rcvBytes);
            string rcv = Encoding.ASCII.GetString(rcvBytes);
            Console.WriteLine("Received from client: " + rcv);
            
            // send to client
            string message = "Hello from server";
            int toSendLen = Encoding.ASCII.GetByteCount(message);
            byte[] toSendBytes = Encoding.ASCII.GetBytes(message);
            byte[] toSendLenBytes = BitConverter.GetBytes(toSendLen);
            socketToClient.Send(toSendLenBytes);
            socketToClient.Send(toSendBytes);
        }
    }
}