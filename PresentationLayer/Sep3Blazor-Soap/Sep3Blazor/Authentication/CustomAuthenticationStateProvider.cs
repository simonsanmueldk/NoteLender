using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Sep3Blazor.Data;
using Sep3Blazor.Model;

namespace Sep3Blazor.Authentication
{
    public class CustomAuthenticationStateProvider :AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRunTime;
        private readonly IUserService userService;
        public User cachedUser { get; set; }
        
        public CustomAuthenticationStateProvider(IJSRuntime jsRunTime, IUserService userService)
        {
            this.jsRunTime = jsRunTime;
            this.userService = userService;
        }
        
        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var identity = new ClaimsIdentity();
            if (cachedUser == null)
            {
                string userAsJson = await jsRunTime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    User temp = JsonSerializer.Deserialize<User>(userAsJson);
                    await ValidateLogin(temp.Username, temp.Password);
                }
            }
            else
            {
                identity = SetupClaimsForUser(cachedUser);
            }

            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(identity);
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
        }
        
        private ClaimsIdentity SetupClaimsForUser(User user)
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name,user.Username));
            claims.Add(new Claim("Id",user.id.ToString()));
            claims.Add(new Claim("FirstName",user.FirstName));
            claims.Add(new Claim("LastName",user.LastName));
            claims.Add(new Claim("Password",user.Password));
            
            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
        

        public async Task ValidateLogin(string tempUserName, string tempPassword)
        {
            Console.WriteLine("Validating log in");
            if (string.IsNullOrEmpty(tempUserName)) throw new Exception("Enter username");
            if (string.IsNullOrEmpty(tempPassword)) throw new Exception("Enter password");

            ClaimsIdentity identity = new ClaimsIdentity();
            try {
                Console.WriteLine("aleo");
                User user = await userService.ValidateLogin(tempUserName, tempPassword);
                Console.WriteLine("aleox2");
                identity = SetupClaimsForUser(user);
                string serialisedData = JsonSerializer.Serialize(user);
                await jsRunTime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                cachedUser = user;
                Console.WriteLine(user.Username);
            } catch (Exception e) {
                throw e;
            }

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity))));
        }
        public void LogOut()
        {
            cachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            jsRunTime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }
}