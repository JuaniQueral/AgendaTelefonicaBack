using AgendaTelefonicaBack.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace AgendaTelefonicaBack.Models
{



    public class AplicationDbContext : DbContext
        {
            public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options)
            {

            }

            public DbSet<Contacto> Contacto { get; set; }
            public DbSet<Usuario> Usuario { get; set; }
             
    }
    
}
