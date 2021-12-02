using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;
using Sep3Blazor;


namespace GrpcService.Logic
{
    public interface ILogicService
    {
        // Note
        public Task<Reply> PostNote(NoteRequest request, ServerCallContext context);
        public Task<Reply> PutNote(NoteRequest request, ServerCallContext context);
        public Task<Reply> GetNote(Request request, ServerCallContext context);
        public Task<Reply> DeleteNote(Request request, ServerCallContext context);
        public Task<Reply> GetNoteList(Request request, ServerCallContext context);

        // Group
        public Task<Reply> PostGroup(Request request, ServerCallContext context);
        public Task<Reply> GetGroup(Request request, ServerCallContext context);

        public Task<Reply> DeleteGroup(Request request, ServerCallContext context);

        // User
        public Task<RegisterReply> RegisterUser(RegisterRequest request, ServerCallContext context);
        public Task<Reply> ValidateUser(Request request, ServerCallContext context);
        public Task<Reply> EditUser(EditUserRequest request, ServerCallContext context);

        // Invitations
        public Task<Reply> GetInvitationList(Request request, ServerCallContext context);
        public Task<Reply> PostInvitation(RegisterInvitationRequest request, ServerCallContext context);
        public Task<Reply> DeleteInvitation(Request request, ServerCallContext context);


        public Task<Reply> GetUserList(Request request, ServerCallContext context);

        public Task<Reply> GetGroupMembersList(Request request, ServerCallContext context);
        public Task<Reply> AddGroupMember(AddGroupMemberRequest request, ServerCallContext context);

        public Task<Reply> DeleteUser(UserRequest request, ServerCallContext context);
        public Task<Reply> GetUser(GetUserRequest request, ServerCallContext context);
        public Task<Reply> DeleteGroupMember(UserRequest request, ServerCallContext context);
    }
}