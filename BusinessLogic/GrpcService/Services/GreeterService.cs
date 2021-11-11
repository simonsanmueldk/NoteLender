using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class GreeterService : Greeter.GreeterBase
    {
        private string uri = "http://localhost:8080";
        private readonly HttpClient client;
        private readonly ILogger<GreeterService> _logger;

        public GreeterService(ILogger<GreeterService> logger)
        {
            client = new HttpClient();
            _logger = logger;
        }

        public override async Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            Console.WriteLine(request);
            Task<string> stringAsync = client.GetStringAsync(uri + "/Group/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new HelloReply
            {
                Message = message
            });
        }

        public override async Task<HelloReply> PostHello(HelloRequest request, ServerCallContext context)
        {
            HttpContent content = new StringContent(request.Name, Encoding.UTF8, "application/json");
            Console.WriteLine(2);
            HttpResponseMessage responseMessage = await client.PutAsync(uri + "/Group", content);
            Console.WriteLine(responseMessage.Content);

            Task<string> stringAsync = client.GetStringAsync(uri + "/Group/" + "5");
            string message = await stringAsync;
            Console.WriteLine(message);
            return await Task.FromResult(new HelloReply
            {
                Message = message
            });
        }
    }
}