using GrupoXpert.Api.Models;
using GrupoXpert.Client.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using GrupoXpert.Api.Data;

namespace GrupoXpert.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Services.IAuthorizationService _autorizacionService;

        private readonly GrupoXpertDbContext _context;

        public AuthenticationController(GrupoXpertDbContext context, Services.IAuthorizationService authorizationService)
        {
            _context = context;
            _autorizacionService = authorizationService;
        }

        [HttpPost]
        [Route("Authenticate")]
        public async Task<IActionResult> Authenticate(AuthorizationRequest authorization)
        {
            var result = await _autorizacionService.GetToken(authorization);
            if (result == null)
                return Unauthorized();

            return Ok(result);
        }
    }
}