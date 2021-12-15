using System;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Logic;
using Microsoft.Extensions.Logging;

namespace GrpcService.Services
{
    public class BusinessServerService : BusinessServer.BusinessServerBase
    {
        /// <summary>
        /// Instance variables
        /// </summary>
        private readonly ILogicService _logicService;
        private readonly ILogger<BusinessServerService> _logger;

        /// <summary>
        /// 1-argument constructor
        /// </summary>
        /// <param name="logger"></param>
        public BusinessServerService(ILogger<BusinessServerService> logger)
        {
            _logicService = new LogicService();
            _logger = logger;
        }
        
        /// <summary>
        /// Method PostNote invokes the method with same name
        /// with the retrieved parameters from NoteService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> PostNote(NoteRequest request, ServerCallContext context)
        {
            return await _logicService.PostNote(request, context);
        }

        /// <summary>
        /// Method PutNote invokes the method with same name
        /// with the retrieved parameters from NoteService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> PutNote(NoteRequest request, ServerCallContext context)
        {
            return await _logicService.PutNote(request, context);
        }

        /// <summary>
        /// Method DeleteNote invokes the method with same name
        /// with the retrieved parameters from NoteService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> DeleteNote(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteNote(request, context);
        }

        /// <summary>
        /// Method GetNoteList invokes the method with same name
        /// with the retrieved parameters from NoteService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> GetNoteList(Request request, ServerCallContext context)
        {
            String message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
            _logger.LogInformation(message);
            return await _logicService.GetNoteList(request, context);
        }

        /// <summary>
        /// Method PostGroup invokes the method with same name
        /// with the retrieved parameters from GroupService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> PostGroup(PostGroupRequest request, ServerCallContext context)
        {
            return await _logicService.PostGroup(request, context);
        }

        
        
        /// <summary>
        /// Method DeleteGroup invokes the method with same name
        /// with the retrieved parameters from GroupService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> DeleteGroup(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteGroup(request, context);
        }

        /// <summary>
        /// Method RegisterUser invokes the method with same name
        /// with the retrieved parameters from UserService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<RegisterReply> RegisterUser(RegisterRequest request, ServerCallContext context)
        {
            return await _logicService.RegisterUser(request, context);
        }
        /// <summary>
        /// Method ValidateUser invokes the method with same name
        /// with the retrieved parameters from UserService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> ValidateUser(Request request, ServerCallContext context)
        {
            return await _logicService.ValidateUser(request, context);
        }

        /// <summary>
        /// Method EditUser invokes the method with same name
        /// with the retrieved parameters from UserService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> EditUser(EditUserRequest request, ServerCallContext context)
        {
            return await _logicService.EditUser(request, context);
        }
        /// <summary>
        /// Method DeleteUser invokes the method with same name
        /// with the retrieved parameters from UserService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> DeleteUser(UserRequest request, ServerCallContext context)
        {
            return await _logicService.DeleteUser(request, context);
        }
        /// <summary>
        /// Method GetUserList invokes the method with same name
        /// with the retrieved parameters from UserService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> GetUserList(GetUserRequest request, ServerCallContext context)
        {
            return await _logicService.GetUserList(request, context);
        }
        
        /// <summary>
        /// Method PostInvitation invokes the method with same name
        /// with the retrieved parameters from InvitationService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> PostInvitation(PostInvitationRequest request, ServerCallContext context)
        {
            return await _logicService.PostInvitation(request, context);
        }
        /// <summary>
        /// Method GetInvitationList invokes the method with same name
        /// with the retrieved parameters from InvitationService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> GetInvitationList(Request request, ServerCallContext context)
        {
            return await _logicService.GetInvitationList(request, context);
        }

        /// <summary>
        /// Method DeleteInvitation invokes the method with same name
        /// with the retrieved parameters from InvitationService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> DeleteInvitation(Request request, ServerCallContext context)
        {
            return await _logicService.DeleteInvitation(request, context);
        }
        /// <summary>
        /// Method GetGroupMemberList invokes the method with same name
        /// with the retrieved parameters from GroupMembersService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> GetGroupMemberList(Request request, ServerCallContext context)
        {
            return await _logicService.GetGroupMemberList(request, context);
        }

        /// <summary>
        /// Method GetGroupList invokes the method with same name
        /// with the retrieved parameters from GroupService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> GetGroupList(Request request, ServerCallContext context)
        {
            return await _logicService.GetGroupList(request, context);
        }
        /// <summary>
        /// Method AddGroupMember invokes the method with same name
        /// with the retrieved parameters from GroupMembersService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> AddGroupMember(AddGroupMemberRequest request, ServerCallContext context)
        {
            return await _logicService.AddGroupMember(request, context);
        }

        /// <summary>
        /// Method DeleteGroupMember invokes the method with same name
        /// with the retrieved parameters from GroupMembersService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> DeleteGroupMember(UserRequest request, ServerCallContext context)
        {
            return await _logicService.DeleteGroupMember(request, context);
        }

        /// <summary>
        /// Method LeaveGroup invokes the method with same name
        /// with the retrieved parameters from GroupMembersService in 1st tier.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        public override async Task<Reply> LeaveGroup(DeleteGroupMemberRequest request, ServerCallContext context)
        {
            return await _logicService.LeaveGroup(request, context);
        }
    }
}