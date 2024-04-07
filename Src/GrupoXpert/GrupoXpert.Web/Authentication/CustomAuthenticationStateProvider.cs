using GrupoXpert.Client.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace GrupoXpert.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;

        private ClaimsPrincipal? _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var respuesta = await _localStorage.GetAsync<Credencial>("Intetity");
                var identity = respuesta.Success ? respuesta.Value : null;
                if (identity == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, identity.Usuario)
                }, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));                
            }
            catch (Exception)
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));
            }
        }

        public async Task UpdateAuthenticationState(Credencial credencial)
        {
            ClaimsPrincipal claimsPrincipal;

            if (credencial != null)
            {
                await _localStorage.SetAsync("Intetity", credencial);

                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim(ClaimTypes.Name, credencial.Usuario)
                }));
            }
            else
            {
                await _localStorage.DeleteAsync("Intetity");
                claimsPrincipal = _anonymous;
            }
            
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
