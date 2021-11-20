using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;


namespace GrpcService.Logic
{
    public interface ILogicService
    {
        // Note
        public Task<Reply> PostNote(RegisterNoteRequest request, ServerCallContext context);
        public Task<Reply> GetNote(Request request, ServerCallContext context);
        public Task<Reply> DeleteNote(Request request, ServerCallContext context);
        
        // Group
        public Task<Reply> PostGroup(Request request, ServerCallContext context);
        public Task<Reply> GetGroup(Request request, ServerCallContext context);

        public Task<Reply> DeleteGroup(Request request, ServerCallContext context);
        // User
        public Task<RegisterReply> RegisterUser(RegisterRequest request, ServerCallContext context);
        public Task<Reply> ValidateUser(Request request, ServerCallContext context);
        public Task<Reply> GetInvitation(Request request, ServerCallContext context);
        public Task<Reply> PostInvitation(Request request, ServerCallContext context);
        
        
    }
}