using Microsoft.EntityFrameworkCore;
using Parque.Models;

namespace Parque.Data
{
    public class ParqueDbContext : DbContext
    {
        public DbSet<Atraccion> Atracciones { get; set; }
        public DbSet<Boleta> Boletas { get; set; }
        public DbSet<Ingreso> Ingresos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=parque_db;Username=postgres;Password=1234"
            );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boleta>()
                .HasDiscriminator<string>("TipoBoleta")
                .HasValue<BoletaGeneral>("General")
                .HasValue<BoletaVIP>("VIP");
        }
    }
}