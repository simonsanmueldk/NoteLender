using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Logic;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
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

        public override async Task<Reply> PostNote(NoteRequest request, ServerCallContext context)
        {
            return await _logicService.PostNote(request, context);
        }

        public override async Task<Reply> PutNote(NoteRequest request, ServerCallContext context)
        {
            return await _logicService.PutNote(request, context);
        }

        public override async Task<Reply> DeleteNote(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteNote(request, context);
        }

        public override async Task<Reply> GetNoteList(Request request, ServerCallContext context)
        {
            String Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
            return await _logicService.GetNoteList(request, context);
        }

        /*
         *  Group methods
         */

        public override async Task<Reply> PostGroup(PostGroupRequest request, ServerCallContext context)
        {
            return await _logicService.PostGroup(request, context);
        }

        public override async Task<Reply> GetGroup(Request request, ServerCallContext context)
        {
            return await _logicService.GetGroup(request, context);
        }

        public override async Task<Reply> GetNote(Request request, ServerCallContext context)
        {
            String Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(Message);
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

        public override async Task<Reply> DeleteUser(UserRequest request, ServerCallContext context)
        {
            return await _logicService.DeleteUser(request, context);
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

        public override async Task<Reply> DeleteInvitation(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteInvitation(request, context);
        }
        
        public override async Task<Reply> GetUserList(Request request, ServerCallContext context)
        {
            return await _logicService.GetUserList(request, context);
        }

        public override async Task<Reply> GetUser(GetUserRequest request, ServerCallContext context)
        {
            return await _logicService.GetUser(request, context);
        }
        
        public override async Task<Reply> GetGroupMembersList(Request request, ServerCallContext context)
        {
            return await _logicService.GetGroupMembersList(request, context);
        }

        public override async Task<Reply> AddGroupMember(AddGroupMemberRequest request, ServerCallContext context)
        {
            return await _logicService.AddGroupMember(request, context);
        }

        public override async Task<Reply> DeleteGroupMember(UserRequest request, ServerCallContext context)
        {
            return await _logicService.DeleteGroupMember(request, context);
        }

        public override async Task<Reply> LeaveGroup(DeleteGroupMemberRequest request, ServerCallContext context)
        {
            return await _logicService.LeaveGroup(request, context);
        }
    }
}