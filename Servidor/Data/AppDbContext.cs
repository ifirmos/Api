using Microsoft.EntityFrameworkCore;
using Servidor.Models;

namespace Servidor.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Documento> Documentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contrato>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Documento>()
                .HasKey(d => d.Id);

            modelBuilder.Entity<Contrato>()
                .HasOne(c => c.Documento)
                .WithOne(d => d.Contrato)
                .HasForeignKey<Documento>(d => d.ContratoId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}