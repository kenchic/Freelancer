using GrupoXpert.Client.Models;

namespace GrupoXpert.Api.Services
{
    public interface IAuthorizationService
    {
        Task<AuthorizationResponse> GetToken(AuthorizationRequest authorization);
    }
}
