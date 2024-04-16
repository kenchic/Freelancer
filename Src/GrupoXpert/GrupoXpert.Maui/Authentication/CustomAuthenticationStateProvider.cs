using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GrupoXpert.Maui.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public CustomAuthenticationStateProvider()
        {
        }

        public async Task Login(string user, string token)
        {
            await SecureStorage.SetAsync("token", token);
            await SecureStorage.SetAsync("user", user);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            SecureStorage.Remove("token");
            SecureStorage.Remove("user");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                var user = await SecureStorage.GetAsync("user");
                if (user != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, user) };
                    var identity = new ClaimsIdentity(claims, "Custom authentication");
                    return new AuthenticationState(new ClaimsPrincipal(identity));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Request failed:" + ex.ToString());
            }

            return new AuthenticationState(new ClaimsPrincipal());
        }
    }
}