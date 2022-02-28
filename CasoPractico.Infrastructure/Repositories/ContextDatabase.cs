using CasoPractico.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CasoPractico.Infrastructure.Repositories
{
    public class ContextDatabase : DbContext
    {
        public ContextDatabase(DbContextOptions<ContextDatabase> options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
        public DbSet<CupoDiario> CuposDiarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("Cliente");
            modelBuilder.Entity<Cuenta>().ToTable("Cuenta");
            modelBuilder.Entity<Movimiento>().ToTable("Movimiento");
            modelBuilder.Entity<CupoDiario>().ToTable("CupoDiario");

            modelBuilder.Entity<Cuenta>(entity =>
            {
                entity.HasOne(d => d.cliente)
                    .WithMany(p => p.cuentas)
                    .HasForeignKey(d => d.clienteId);
            });

            modelBuilder.Entity<Movimiento>(entity =>
            {
                entity.HasOne(d => d.cuenta)
                    .WithMany(p => p.movimientos)
                    .HasForeignKey(d => d.numeroCuenta);
            });
        }
    }
}
