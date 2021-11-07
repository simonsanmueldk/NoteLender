using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerVersion2
{
    public class Server_Net
    {
        private ClientRest ClientRest;
        private NetworkStream Stream;
        private TcpClient Client;

        public Server_Net()
        {
            Console.WriteLine("Starting server..");

            IPAddress ip = IPAddress.Parse("127.0.0.1");
            TcpListener listener = new TcpListener(ip, 2910);
            listener.Start();

            Console.WriteLine("Server started..");

            Client = listener.AcceptTcpClient();

            Console.WriteLine("Client connected");
            Stream = Client.GetStream();

            ClientRest = new ClientRest();
        }

        public async Task Run(string s)
        {
            // respond
            
            IList<String> notes = await ClientRest.GetNoteAsync(s);
            Console.WriteLine(notes[0]);

            string productsAsJson = JsonSerializer.Serialize(notes);
            Console.WriteLine("hi"+productsAsJson);
            byte[] dataToClient = Encoding.ASCII.GetBytes(productsAsJson);
            Stream.Write(dataToClient, 0, dataToClient.Length);

            // close connection
            // Client.Close();
        }

        public async Task Read()
        {
            while (true)
            {
                // read
                
                byte[] dataFromClient = new byte[1024];
                int bytesRead = Stream.Read(dataFromClient, 0, dataFromClient.Length);
                string s = Encoding.ASCII.GetString(dataFromClient, 0, bytesRead);
                Console.WriteLine(s + "1");

                await Run(s);
            }
        }
    }
}