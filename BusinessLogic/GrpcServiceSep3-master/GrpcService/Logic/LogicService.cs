using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Grpc.Core;
using GrpcService.Model;


namespace GrpcService.Logic
{
    public class LogicService : ILogicService
    {
        private string uri = "http://localhost:8080";
        private readonly HttpClient client;

        public LogicService()
        {
            client = new HttpClient();
        }

        public async Task<Reply> PostGroup(Request request, ServerCallContext context)
        {
            HttpContent content = new StringContent(request.Name, Encoding.UTF8, "application/json");
            Console.WriteLine(2);
            HttpResponseMessage responseMessage = await client.PutAsync(uri + "/Group", content);
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
            Task<string> stringAsync = client.GetStringAsync(uri + "/Group/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }
        
        public async Task<Reply> DeleteGroup(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            HttpResponseMessage responseMessage = await client.DeleteAsync(uri + "/Group/" + request.Name);
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

        public async Task<Reply> PostNote(RegisterNoteRequest request, ServerCallContext context)
        {
            
            ArrayList list = new ArrayList();
            list.Add(request.NoteId);
            list.Add(request.UserId);
            list.Add(request.GroupId);
            list.Add(request.Week);
            list.Add(request.Year);
            list.Add(request.Name);
            list.Add(request.Status);
            list.Add(request.Text);
            string str = JsonSerializer.Serialize(list);
            Console.WriteLine("POSTNOTE LIST JSON = " + str);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            Console.WriteLine("POSTNOTE CONTENT = " + content);
            HttpResponseMessage responseMessage = await client.PostAsync(uri + "/Note", content);
            Console.WriteLine("POSTNOTE RESPONSEMESSAGE = " + responseMessage.Content);
            string message = await responseMessage.Content.ReadAsStringAsync();
            Console.WriteLine(message);
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> GetNote(Request request, ServerCallContext context)
        {
            Console.WriteLine(request.Name);
            Task<string> stringAsync = client.GetStringAsync(uri + "/Note/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }
        
        

        public async Task<Reply> DeleteNote(Request request, ServerCallContext context)
        {
            Console.WriteLine(request);
            HttpResponseMessage responseMessage = await client.DeleteAsync(uri + "/Note/" + request.Name);
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
            User temp = new User(0,request.FirstName,request.LastName,request.Username,request.Password);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(uri + "/UnregisterUser", content);
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
            User temp = new User(0,"","",request.Name,request.Type);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(uri + "/User", content);
            try
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    string message = await responseMessage.Content.ReadAsStringAsync();
                    User user = JsonSerializer.Deserialize<User>(message);
                    if (request.Type.Equals(user.Password))
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

        public async Task<Reply> GetInvitation(Request request, ServerCallContext context)
        {
            Task<string> stringAsync = client.GetStringAsync(uri + "/InvitationList/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> PostInvitation(Request request, ServerCallContext context)
        {
            HttpContent content = new StringContent(request.Name, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await client.PostAsync(uri + "/Invitation", content);
            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }

        public async Task<Reply> GetNoteList(Request request, ServerCallContext context)
        {
            Console.WriteLine(request.Name);
            Task<string> stringAsync = client.GetStringAsync(uri + "/NoteList/" + request.Name);
            string message = await stringAsync;
            return await Task.FromResult(new Reply
            {
                Message = message
            });
        }
    }
}