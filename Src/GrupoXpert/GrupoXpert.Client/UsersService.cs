using GrupoXpert.Client.Models;
using GrupoXpert.Tools;
using RestSharp;
using System.Net;

namespace GrupoXpert.Client
{
    public class UsersService
    {
        private readonly RestClient route = new RestClient($"{Configuration.AppAPIUrl()}/v1/users");

        public User Get(string token, string id)
        {
            var request = new RestRequest("securitylevel", Method.Get);
            route.AddDefaultHeader("Authorization", $"Bearer {token}");
            request.AddParameter("Id", id, ParameterType.QueryString);

            RestResponse response = route.Execute(request);
            return new User();
        }
    }
}
