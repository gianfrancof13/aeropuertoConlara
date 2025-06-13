using Microsoft.EntityFrameworkCore;
using AeropuertoConlara.Models;

namespace AeropuertoConlara.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Vuelo> Vuelos { get; set; }
        public DbSet<FormularioCIVSL> FormulariosCIVSL { get; set; }
        public DbSet<FormularioCIVConlara> FormulariosCIVConlara { get; set; }
    }
}