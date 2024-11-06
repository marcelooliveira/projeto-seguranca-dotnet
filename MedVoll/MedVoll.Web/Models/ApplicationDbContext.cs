using Microsoft.EntityFrameworkCore;

namespace MedVoll.Web.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<Medico> Medicos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}


