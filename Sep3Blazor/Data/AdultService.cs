using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Assigment_1.Data
{
    public class AdultService : IAdultService
    {
        public IList<String> AdultsList { get;  set; }
        private string productsFile = "adults.json";

        public AdultService()
        {
             Connect();
        }


        public void Save()
        {
            string productsAsJson = JsonSerializer.Serialize(AdultsList);
            File.WriteAllText(productsFile, productsAsJson);
        }

        public void Remove(int adultId)
        {
        }

        public void Connect()
        {
            TcpClient tcpClient = new TcpClient("127.0.0.1", 2910);
            NetworkStream stream = tcpClient.GetStream();
        
            // send to server
            string message = "Hello from client";
            byte[] dataToServer = Encoding.ASCII.GetBytes(message);
            stream.Write(dataToServer, 0, dataToServer.Length);
        
            // read response
            byte[] fromServer = new byte[8192];
            int bytesRead = stream.Read(fromServer, 0, fromServer.Length);
            string response = Encoding.ASCII.GetString(fromServer, 0, bytesRead);
            AdultsList = JsonSerializer.Deserialize<List<String>>(response);
            Console.WriteLine(response);
            tcpClient.Close();
        }
    }
}