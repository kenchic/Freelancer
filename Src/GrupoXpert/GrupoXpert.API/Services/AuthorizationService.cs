using GrupoXpert.Api.Data;
using GrupoXpert.Client.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GrupoXpert.Api.Services
{
    public class AuthorizationService : IAuthorizationService
    {

        private readonly GrupoXpertDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthorizationService(GrupoXpertDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        private string GeneratorToken(string idUsuario)
        {

            var key = _configuration.GetValue<string>("JwtSettings:key");
            var keyBytes = Encoding.ASCII.GetBytes(key);

            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, idUsuario));

            var credencialesToken = new SigningCredentials(
                new SymmetricSecurityKey(keyBytes),
                SecurityAlgorithms.HmacSha256Signature
                );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credencialesToken
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokenCreate = tokenHandler.WriteToken(tokenConfig);

            return tokenCreate;
        }

        public async Task<AuthorizationResponse> GetToken(AuthorizationRequest auth)
        {
            var user = _context.Users.FirstOrDefault(x =>
                x.Id == auth.UserName.ToUpper() &&
                x.Password == auth.Password
            );

            if (user == null)
            {
                return await Task.FromResult<AuthorizationResponse>(null);
            }

            string tokenCreate = GeneratorToken(user.Id.ToString());

            return new AuthorizationResponse() { Token = tokenCreate, Result = true, Message = "Ok" };

        }
    }
}
