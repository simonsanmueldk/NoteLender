using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Logic;
using Microsoft.Extensions.Logging;

namespace GrpcService
{
    public class BusinessServerService : BusinessServer.BusinessServerBase
    {
        private readonly ILogicService _logicService;
        private readonly ILogger<BusinessServerService> _logger;

        public BusinessServerService(ILogger<BusinessServerService> logger)
        {
            _logicService = new LogicService();
            _logger = logger;
        }

        
        public override async Task<Reply> GetGroup(Request request, ServerCallContext context)
        {
            return await _logicService.GetGroup(request, context);
        }
        
        public override async Task<Reply> GetNote(Request request, ServerCallContext context)
        {
            return await _logicService.GetNote(request, context);
        }


        public override async Task<Reply> RegisterUser(Request request, ServerCallContext context)
        {
            return await _logicService.RegisterUser(request, context);
        }
        
        public override async Task<Reply> ValidateUser(Request request, ServerCallContext context)
        {
            return await _logicService.ValidateUser(request, context);
        }

        

        public override async Task<Reply> AddNote(Request request, ServerCallContext context)
        {
            return await _logicService.AddNote(request, context);
        }

        public override async Task<Reply> PostGroup(Request request, ServerCallContext context)
        {
            return await _logicService.PostGroup(request, context);
        }

        public override async Task<Reply> DeleteGroup(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteGroup(request, context);
        }
        
        public  async Task<Reply> PostInvitation(Request request, ServerCallContext context)
        {
            return await _logicService.PostInvitation(request, context);
        }

        public  async Task<Reply> GetInvitation(Request request, ServerCallContext context)
        {
            return await _logicService.GetInvitation(request, context);
        }
        
        
        
        
    }
}