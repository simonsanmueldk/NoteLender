using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Model;
using Sep3Blazor.Model;

namespace GrpcService.Logic
{
    public class LogicService : ILogicService
    {
        private string uri = "http://localhost:8080";
        private readonly HttpClient _client;

        public LogicService()
        {
            _client = new HttpClient();
        }

        public async Task<Reply> PostGroup(Request request, ServerCallContext context)
        {
            HttpContent content = new StringContent(request.Name, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PutAsync(uri + "/group", content);
            Console.WriteLine("1" + responseMessage.Content);
            string message = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> GetGroup(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            Task<string> stringAsync = _client.GetStringAsync(uri + "/group/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> DeleteGroup(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/group/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(@"Error : (responseMessage.Status), (responseMessage.ReasonPhrase");
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> PostNote(NoteRequest request, ServerCallContext context)
        {
            Note note = new Note(request.NoteId, request.UserId, request.GroupId,
                request.Week, request.Year, request.Name, request.Status, request.Text);
            string str = JsonSerializer.Serialize(note);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/note", content);
            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> PutNote(NoteRequest request, ServerCallContext context)
        {
            Note note = new Note(request.NoteId, request.UserId, request.GroupId,
                request.Week, request.Year, request.Name, request.Status, request.Text);
            string str = JsonSerializer.Serialize(note);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PutAsync(uri + "/note", content);
            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> GetNote(Request request, ServerCallContext context)
        {
            Console.WriteLine(request.Name);
            Task<string> stringAsync = _client.GetStringAsync(uri + "/note/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> DeleteNote(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/note/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(@"Error : (responseMessage.Status), (responseMessage.ReasonPhrase");
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }


        public async Task<RegisterReply> RegisterUser(RegisterRequest request, ServerCallContext context)
        {
            User temp = new User(0, request.FirstName, request.LastName, request.Username, request.Password);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/unregisteruser", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                Console.WriteLine(message);
                return await Task.FromResult(new RegisterReply
                {
                    Message = message
                });
            }

            return null;
        }


        public async Task<Reply> ValidateUser(Request request, ServerCallContext context)
        {
            User temp = new User(0, "", "", request.Name, request.Type);
            Console.WriteLine(request.Name);
            string str = JsonSerializer.Serialize(temp);

            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/user", content);
            try
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string message = await responseMessage.Content.ReadAsStringAsync();
                    User user = JsonSerializer.Deserialize<User>(message);
                    if (request.Type.Equals(user.password))
                    {
                        return await Task.FromResult(new Reply
                        {
                            Message = message
                        });
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return null;
        }

        public async Task<Reply> EditUser(EditUserRequest request, ServerCallContext context)
        {
            Console.WriteLine("edit aleox1");
            User temp = new User(request.Id, "", "", "", request.NewPassword);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            Console.WriteLine("edit aleo");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + $"/user/{request.Id}", content);
            Console.WriteLine("edit aleo x2");
            if (responseMessage.IsSuccessStatusCode)
            {
                string message = await responseMessage.Content.ReadAsStringAsync();

                return await Task.FromResult(new Reply
                {
                    Message = message
                });
            }

            return null;
        }

        public async Task<Reply> GetInvitationList(Request request, ServerCallContext context)
        {
            Task<string> stringAsync = _client.GetStringAsync(uri + "/InvitationList/" + request.Name);
            Console.WriteLine(request.Name + "AAAAAAAAA");
            stringAsync = _client.GetStringAsync(uri + "/invitation/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> PostInvitation(RegisterInvitationRequest request, ServerCallContext context)
        {
            Invitation invitation = new Invitation(request.Id, request.GroupId, request.InviteeId, request.InvitorId);
            string str = JsonSerializer.Serialize(invitation);
            Console.WriteLine(invitation);
            Console.WriteLine(str);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/invitation", content);
            string message = await responseMessage.Content.ReadAsStringAsync();

            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> DeleteInvitation(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/invitation/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(@"Error : (responseMessage.Status), (responseMessage.ReasonPhrase");
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> GetUserList(Request request, ServerCallContext context)
        {
            Console.WriteLine(request.Name);
            Task<string> stringAsync = _client.GetStringAsync(uri + "/userlist/" + request.Name);
            string message = await stringAsync;
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> DeleteUser(UserRequest request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/user/" + request.Id);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(@"Error : (responseMessage.Status), (responseMessage.ReasonPhrase");
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> GetNoteList(Request request, ServerCallContext context)
        {
            Console.WriteLine(request.Name);
            Task<string> stringAsync = _client.GetStringAsync(uri + "/notelist/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }
    }
}