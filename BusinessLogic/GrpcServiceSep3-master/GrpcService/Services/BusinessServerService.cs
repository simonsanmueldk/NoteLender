using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Logic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class BusinessServerService : BusinessServer.BusinessServerBase
    {
        private string uri = "http://localhost:8080";
        private readonly HttpClient _client;
        private readonly ILogicService _logicService;
        private readonly ILogger<BusinessServerService> _logger;

        public BusinessServerService(ILogger<BusinessServerService> logger)
        {
           // _client = new HttpClient();
            _logicService = new LogicService();
            _logger = logger;
            //HEY
        }

        public override async Task<Reply> GetGroup(Request request, ServerCallContext context)
        {
            return await _logicService.GetGroup(request, context);
            Console.WriteLine(request);
            Task<string> stringAsync = _client.GetStringAsync(uri + "/Group/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }
        
        
        
        public override async Task<Reply> GetNote(Request request, ServerCallContext context)
        {
            return await _logicService.GetNote(request, context);
            Console.WriteLine(request);
            Task<string> stringAsync = _client.GetStringAsync(uri + "/NoteList/" + request.Name);
            string message = await stringAsync;
            Console.WriteLine("lalalalala"+message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public override async Task<Reply> AddNote(Request request, ServerCallContext context)
        {
            return await _logicService.AddNote(request, context);
            HttpContent content = new StringContent(request.Name, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/Note", content);
            Console.WriteLine(responseMessage.Content);
            Task<string> task = _client.GetStringAsync(uri + "/NoteList" + request.Name);
            string message = await task;
            return await Task.FromResult(new Reply
            {
                Message = message
            });

        }

        public override async Task<Reply> PostGroup(Request request, ServerCallContext context)
        {
            return await _logicService.PostGroup(request, context);
            HttpContent content = new StringContent(request.Name, Encoding.UTF8, "application/json");
            Console.WriteLine(2);
            HttpResponseMessage responseMessage = await _client.PutAsync(uri + "/Group", content);
            Console.WriteLine("1"+responseMessage.Content);
            string message = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public  override async Task<Reply> DeleteGroup(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteGroup(request, context);
        }
    }
}