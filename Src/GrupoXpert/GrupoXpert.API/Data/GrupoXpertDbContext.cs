using GrupoXpert.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace GrupoXpert.Api.Data
{
    public class GrupoXpertDbContext : DbContext
    {
        public GrupoXpertDbContext(DbContextOptions<GrupoXpertDbContext> options) : base(options)
        {
            
        }

        public DbSet<Users> Users { get; set; }
    }
}
