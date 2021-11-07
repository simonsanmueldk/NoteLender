using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class SocketClientA
    {
        static void Main(string[] args)
        {
            IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 2910);
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(serverAddress);
            
            // send to server
            string message = "Hello from client";
            int toSendLen = Encoding.ASCII.GetByteCount(message);
            byte[] toSendBytes = Encoding.ASCII.GetBytes(message);
            byte[] toSendLenBytes = BitConverter.GetBytes(toSendLen);
            clientSocket.Send(toSendLenBytes);
            clientSocket.Send(toSendBytes);
            
            // read response
            
            byte[] rcvLenBytes = new byte[4];
            clientSocket.Receive(rcvLenBytes);
            int rcvLen = BitConverter.ToInt32(rcvLenBytes, 0);
            byte[] rcvBytes = new byte[rcvLen];
            clientSocket.Receive(rcvBytes);
            string rcv = Encoding.ASCII.GetString(rcvBytes);
            Console.WriteLine("Received from server: " + rcv);
        }
    }
}