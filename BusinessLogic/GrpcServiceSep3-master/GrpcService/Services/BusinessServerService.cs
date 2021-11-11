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
    public class BusinessServerService : BusinessServer.BusinessServerBase
    {
        private string uri = "http://localhost:8080";
        private readonly HttpClient client;
        private readonly ILogger<BusinessServerService> _logger;

        public BusinessServerService(ILogger<BusinessServerService> logger)
        {
            client = new HttpClient();
            _logger = logger;
            //HEY
        }

        public override async Task<Reply> GetGroup(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            Task<string> stringAsync = client.GetStringAsync(uri + "/Group/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }
        
        public override async Task<Reply> GetNote(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            Task<string> stringAsync = client.GetStringAsync(uri + "/NoteList/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public override async Task<Reply> PostGroup(Request request, ServerCallContext context)
        {
            HttpContent content = new StringContent(request.Name, Encoding.UTF8, "application/json");
            Console.WriteLine(2);
            HttpResponseMessage responseMessage = await client.PutAsync(uri + "/Group", content);
            Console.WriteLine(responseMessage.Content);

            Task<string> stringAsync = client.GetStringAsync(uri + "/Group/" + "5");
            string message = await stringAsync;
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }
    }
}