using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Logic;
using Microsoft.Extensions.Logging;
using Sep3Blazor;

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
        /*
         *  Note methods
         */

        public override async Task<Reply> PostNote(RegisterNoteRequest request, ServerCallContext context)
        {
            return await _logicService.PostNote(request, context);
        }

        public override async Task<Reply> DeleteNote(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteGroup(request, context);
        }

        public override async Task<Reply> GetNoteList(Request request, ServerCallContext context)
        {
            return await _logicService.GetNoteList(request, context);
        }

        /*
         *  Group methods
         */

        public override async Task<Reply> PostGroup(Request request, ServerCallContext context)
        {
            return await _logicService.PostGroup(request, context);
        }

        public override async Task<Reply> GetGroup(Request request, ServerCallContext context)
        {
            return await _logicService.GetGroup(request, context);
        }

        public override async Task<Reply> GetNote(Request request, ServerCallContext context)
        {
            return await _logicService.GetNote(request, context);
        }

        public override async Task<Reply> DeleteGroup(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteGroup(request, context);
        }

        /*
         *  User methods
         */

        public override async Task<RegisterReply> RegisterUser(RegisterRequest request, ServerCallContext context)
        {
            return await _logicService.RegisterUser(request, context);
        }

        public override async Task<Reply> ValidateUser(Request request, ServerCallContext context)
        {
            return await _logicService.ValidateUser(request, context);
        }

        public override async Task<Reply> EditUser(EditUserRequest request, ServerCallContext context)
        {
            return await _logicService.EditUser(request, context);
        }

        /*
         * Invitation Methods
         */
        public override async Task<Reply> PostInvitation(RegisterInvitationRequest request, ServerCallContext context)
        {
            return await _logicService.PostInvitation(request, context);
        }

        public override async Task<Reply> GetInvitationList(Request request, ServerCallContext context)
        {
            return await _logicService.GetInvitationList(request, context);
        }
    }
}