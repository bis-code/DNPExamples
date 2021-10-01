using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Microsoft.VisualBasic;
using WebApplication2.Model;

namespace WebApplication2.Data
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime;
        //Services. IJSRuntime used to call javascript
        private readonly IUserService userService;

        private User cachedUser;
        //Catching logged in user for easy access.
        
        public CustomAuthenticationStateProvider(IJSRuntime jsRuntime, IUserService userService)
        {
            this.jsRuntime = jsRuntime;
            this.userService = userService;
            //Constructor, using depedency injection
        }

        
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            //Method called by framework
        {
            var idendity = new ClaimsIdentity();
            //The object containing claims
            
            if (cachedUser == null)
            //Check if user is cached
            {
                string userAsJson = await jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "currentUser");
                //using Javascript to get User-as-json from session storage
                if (!string.IsNullOrEmpty(userAsJson))
                {
                    User tmp = JsonSerializer.Deserialize<User>(userAsJson);
                    ValidateLogin(tmp.UserName, tmp.Password);
                }
            }
            else
            {
                idendity = SetupClaimsForUser(cachedUser);
            }

            ClaimsPrincipal cachedClaimsPrincipal = new ClaimsPrincipal(idendity);
            ///Returning auth info
            return await Task.FromResult(new AuthenticationState(cachedClaimsPrincipal));
            
            //(!!!!!) The cached user may become null, if you refresh the page. That will create a new instance of this class.
        }

        public void ValidateLogin(string username, string password)
        //We call this when logging in
        {
            Console.WriteLine("Validating log in");
            if (string.IsNullOrEmpty(username)) throw new Exception("Enter username");
            //Checking input
            if (string.IsNullOrEmpty(password)) throw new Exception("Enter password");

            ClaimsIdentity identity = new ClaimsIdentity();
            try
            {
                User user = userService.ValidateUser(username, password);
                //Validating user
                identity = SetupClaimsForUser(user);
                string serialisedData = JsonSerializer.Serialize(user);
                //Storing user in session storage
                jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", serialisedData);
                //Catching user
                cachedUser = user;
            }
            catch (Exception e)
            {
                throw e;
                //Throwing exception from UserService
            }

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(new ClaimsPrincipal())));
            //Telling the system a user logged in
        }

        public void Logout()
        //Call this to log out
        {
            cachedUser = null;
            var user = new ClaimsPrincipal(new ClaimsIdentity());
            //Deleting user cache
            jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "currentUser", "");
            //Deleting stored user.
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            //Creating a no-claims-user info
        }

        private ClaimsIdentity SetupClaimsForUser(User user)
        //Setiing up the claims for a user
        {
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim("Role", user.Role));
            claims.Add(new Claim("City",user.City));
            claims.Add(new Claim("Domain", user.Domain));
            claims.Add(new Claim("Birthday", user.BirthYear.ToString()));
            claims.Add(new Claim("Level",user.SecurityLevel.ToString()));
            //Modify to match your own user type

            ClaimsIdentity identity = new ClaimsIdentity(claims, "apiauth_type");
            return identity;
        }
    }
}