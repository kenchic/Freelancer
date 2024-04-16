using GrupoXpert.Client.Models;
using GrupoXpert.Tools;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace GrupoXpert.Client
{
    public class AuthenticationService
    {
        private readonly RestClient route = new RestClient($"{Configuration.AppAPIUrl()}/v1/authentication");

        public AuthorizationResponse Login(AuthorizationRequest authorization)
        {
            var request = new RestRequest("authenticate", Method.Post);
            request.AddJsonBody(authorization);

            RestResponse response = route.Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                AuthorizationResponse auth = JsonConvert.DeserializeObject<AuthorizationResponse>(response.Content);
                return auth;
            }
            return null;
        }
    }
}
