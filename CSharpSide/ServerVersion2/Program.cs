using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServerVersion2
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Server_Net serverNet = new Server_Net();
            await serverNet.Read();
        }
    }
}