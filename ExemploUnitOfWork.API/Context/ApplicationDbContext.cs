using ExemploUnitOfWork.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExemploUnitOfWork.API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Celular).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Endereco).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DataCadastro).HasDefaultValueSql("GETDATE()");

                // Índice único para email
                entity.HasIndex(e => e.Email).IsUnique();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
