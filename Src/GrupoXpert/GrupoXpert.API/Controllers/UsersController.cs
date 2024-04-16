using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GrupoXpert.Api.Data;
using GrupoXpert.Api.Models;

namespace GrupoXpert.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly GrupoXpertDbContext _context;

        public UsersController(GrupoXpertDbContext context)
        {
            _context = context;
        }

        // GET: Users
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id.ToUpper());
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: Users/Create
        [HttpPost]
        public async Task<IActionResult> Create(Users user)
        {
            user.Id = user.Id.ToUpper();
            _context.Add(user);
            await _context.SaveChangesAsync();
            return Ok();
        }

        // PUT: Users/Edit/5
        [HttpPut]
        public async Task<IActionResult> Edit(string id, Users user)
        {
            if (id.ToUpper() != user.Id.ToUpper())
            {
                return NotFound();
            }

            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.Id.ToUpper()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // DELETE: Users/Delete/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _context.Users.FindAsync(id.ToUpper());
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
