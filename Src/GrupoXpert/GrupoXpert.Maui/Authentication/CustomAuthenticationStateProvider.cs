using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace GrupoXpert.Maui.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        public CustomAuthenticationStateProvider()
        {
        }

        public async Task Login(string token, string usuario)
        {
            await SecureStorage.SetAsync("token", token);
            await SecureStorage.SetAsync("usuario", usuario);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task Logout()
        {
            SecureStorage.Remove("token");
            SecureStorage.Remove("usuario");
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync("token");
                var usuario = await SecureStorage.GetAsync("usuario");
                if (usuario != null)
                {
                    var claims = new[] { new Claim(ClaimTypes.Name, usuario) };
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