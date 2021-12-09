using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Sep3Blazor.Data.GroupData;
using Sep3Blazor.Data.Notifications.NotificationModel;
using Sep3Blazor.Data.UserData;
using Sep3Blazor.Model;

namespace Sep3Blazor.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRunTime;
        private readonly IUserService _userService;
        private readonly IGroupService _groupService;
        public User CachedUser { get; set; }
        public IList<Group> GroupList { get; set; }

        public CustomAuthenticationStateProvider(IJSRuntime jsRunTime, IUserService userService, IGroupService groupService)
        {
            _jsRunTime = jsRunTime;
            _userService = userService;
            _groupService = groupService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (CachedUser == null)
            {
                string userAsJson = await _jsRunTime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    User temp = JsonSerializer.Deserialize<User>(userAsJson);
                    ValidateLogin(temp.username, temp.password);
                }
            }
            else
            {
                identity = SetupClaimsForUser(CachedUser,GroupList);
            }

            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }

        private  ClaimsIdentity SetupClaimsForUser(User user,IList<Group> groupList)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.username));
            claims.Add(new Claim("Id", user.id.ToString()));
            claims.AddRange(groupList.Select(@group => new Claim(ClaimTypes.Role, @group.id.ToString())));
            claims.Add(new Claim("FirstName", user.firstName));
            claims.Add(new Claim("LastName", user.lastName));
            claims.Add(new Claim("Password", user.password));

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }

        public async Task<Notification> ValidateLogin(string tempUserName, string tempPassword)
        {
            Console.WriteLine("Validating log in");
            Notification notification = null;
            ClaimsIdentity identity = new ClaimsIdentity();
            try
            {
                User user = await _userService.ValidateUser(tempUserName, tempPassword);
                IList<Group> groupList = await _groupService.GetGroupList(user.id);
                identity = SetupClaimsForUser(user,groupList);
                string serialisedData = JsonSerializer.Serialize(user);
                await _jsRunTime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                CachedUser = user;
                GroupList = groupList;
                notification = new Notification("Success", "User successfully logged in ",
                    NotificationType.Success);
            }
            catch (Exception e)
            {
                Console.WriteLine( e.StackTrace);
                notification = new Notification("Error", "Invalid Login",
                    NotificationType.Error);
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
            return notification;

        }

        public void LogOut()
        {
            CachedUser = null;
            GroupList = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            _jsRunTime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task ModifyClaims()
        {
            IList<Group> groupList = await _groupService.GetGroupList(CachedUser.id);
            ClaimsIdentity identity = SetupClaimsForUser(CachedUser,groupList);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }
    }
}