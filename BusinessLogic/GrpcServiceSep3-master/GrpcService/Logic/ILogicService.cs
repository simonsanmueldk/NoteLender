using System.Net.Http;
using System.Threading.Tasks;
using Grpc.Core;

namespace GrpcService.Logic
{
    public interface ILogicService
    {
        public Task<Reply> GetGroup(Request request, ServerCallContext context);
        public Task<Reply> GetNote(Request request, ServerCallContext context);
        public Task<Reply> AddNote(Request request, ServerCallContext context);
        public Task<Reply> PostGroup(Request request, ServerCallContext context);
    }
}