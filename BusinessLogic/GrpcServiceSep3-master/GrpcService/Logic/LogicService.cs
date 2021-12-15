using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Instance variables, uri defines localhost
        /// </summary>
        private string uri = "http://localhost:8080";

        private readonly HttpClient _client;

        /// <summary>
        /// No-argument constructor
        /// </summary>
        public LogicService()
        {
            _client = new HttpClient();
        }

        /// <summary>
        /// GetNoteList
        /// Method sends GET request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> GetNoteList(Request request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(uri + "/notes/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(responseMessage.StatusCode + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

     
        /// <summary>
        /// PostNote
        /// Method sends POST request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> PostNote(NoteRequest request, ServerCallContext context)
        {
            Note note = new Note(request.NoteId, request.GroupId, request.Week, request.Year, request.Name,
                request.Status, request.Text);
            string str = JsonSerializer.Serialize(note);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/note", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

     
        /// <summary>
        /// PutNote
        /// Method sends PUT request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> PutNote(NoteRequest request, ServerCallContext context)
        {
            Note note = new Note(request.NoteId, request.GroupId, request.Week, request.Year, request.Name,
                request.Status, request.Text);
            string str = JsonSerializer.Serialize(note);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PutAsync(uri + "/note", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

   
        /// <summary>
        /// DeleteNote
        /// Method sends DELETE request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> DeleteNote(Request request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/note/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

        /// <summary>
        /// GetGroupList
        /// Method sends GET request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> GetGroupList(Request request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(uri + "/groups/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

        /// <summary>
        /// PostGroup
        /// Method sends POST request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> PostGroup(PostGroupRequest request, ServerCallContext context)
        {
            HttpContent content = new StringContent(request.GroupName, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/group/" + request.MemberId, content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception(responseMessage.StatusCode + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }


        /// <summary>
        /// DeleteGroup
        /// Method sends DELETE request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> DeleteGroup(Request request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/group/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

   
    
        /// <summary>
        /// GetGroupMemberList
        /// Method sends GET request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> GetGroupMemberList(Request request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(uri + "/groupmemberslist/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

     
        /// <summary>
        /// AddGroupMember
        /// Method sends POST request to persistence layer
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns>Exception or reply message</returns>
        public async Task<Reply> AddGroupMember(AddGroupMemberRequest request, ServerCallContext context)
        {
            GroupMembers temp = new GroupMembers(0, request.GroupId, null, request.UserId);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/groupmembers", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply() {Message = message});
        }

       /// <summary>
       /// DeleteGroupMember
       /// Method sends DELETE request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> DeleteGroupMember(UserRequest request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/groupmembers/" + request.Id);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

   
       /// <summary>
       /// LeaveGroup
       /// Method sends POST request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> LeaveGroup(DeleteGroupMemberRequest request, ServerCallContext context)
        {
            HttpContent content = new StringContent(request.UserId.ToString(), Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage =
                await _client.PostAsync(uri + $"/groupmembers/{request.GroupId}", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

       
       /// <summary>
       /// ValidateUser
       /// Method sends POST request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> ValidateUser(Request request, ServerCallContext context)
        {
            User temp = new User(0, "", "", request.Name, request.Type);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/user", content);
            if (responseMessage.IsSuccessStatusCode)
            {
                string message = await responseMessage.Content.ReadAsStringAsync();
                User user = JsonSerializer.Deserialize<User>(message);
                if (request.Type.Equals(user.password))
                {
                    return await Task.FromResult(new Reply {Message = message});
                }
            }
            else
            {
                return null;
            }

            return null;
        }

   
       /// <summary>
       /// RegisterUser
       /// Method sends POST request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<RegisterReply> RegisterUser(RegisterRequest request, ServerCallContext context)
        {
            User temp = new User(0, request.FirstName, request.LastName, request.Username, request.Password);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/unregisteruser", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new RegisterReply {Message = message});
        }

     
       /// <summary>
       /// GetUserList
       /// Method sends GET request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> GetUserList(GetUserRequest request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(uri + "/users/" + request.Username);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

     
       /// <summary>
       /// EditUser
       /// Method sends POST request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> EditUser(EditUserRequest request, ServerCallContext context)
        {
            User temp = new User(request.Id, "", "", "", request.NewPassword);
            string str = JsonSerializer.Serialize(temp);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + $"/user/{request.Id}", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

   
       /// <summary>
       /// DeleteUser
       /// Method sends DELETE request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> DeleteUser(UserRequest request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/user/" + request.Id);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

       
       /// <summary>
       /// GetInvitationList
       /// Method sends GET request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> GetInvitationList(Request request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.GetAsync(uri + "/invitations/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

       /// <summary>
       /// PostInvitation
       /// Method sends POST request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> PostInvitation(PostInvitationRequest request, ServerCallContext context)
        {
            Invitation invitation = new Invitation(request.Id, request.GroupId, null, request.InviteeId, null,
                request.InvitorId, null);
            string str = JsonSerializer.Serialize(invitation);
            HttpContent content = new StringContent(str, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(uri + "/invitation", content);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }

       /// <summary>
       /// DeleteInvitation
       /// Method sends DELETE request to persistence layer
       /// </summary>
       /// <param name="request"></param>
       /// <param name="context"></param>
       /// <returns>Exception or reply message</returns>
        public async Task<Reply> DeleteInvitation(Request request, ServerCallContext context)
        {
            HttpResponseMessage responseMessage = await _client.DeleteAsync(uri + "/invitation/" + request.Name);
            if (!responseMessage.IsSuccessStatusCode)
            {
                throw new Exception("Error " + responseMessage.StatusCode + " " + responseMessage.ReasonPhrase);
            }

            string message = await responseMessage.Content.ReadAsStringAsync();
            return await Task.FromResult(new Reply {Message = message});
        }
    }
}