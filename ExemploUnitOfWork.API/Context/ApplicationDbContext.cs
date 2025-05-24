using ExemploUnitOfWork.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExemploUnitOfWork.API.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Celular).IsRequired().HasMaxLength(20);
                entity.Property(e => e.Endereco).IsRequired().HasMaxLength(200);
                entity.Property(e => e.DataCadastro).IsRequired();
                entity.Property(e => e.DataUltimaCompra);

                // Índice único para email
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<Produto>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Valor).IsRequired().HasPrecision(18,2);
                entity.Property(e => e.SaldoEstoque).IsRequired().HasPrecision(18, 2);
                entity.Property(e => e.DataCadastro).IsRequired();
            });

            modelBuilder.Entity<Venda>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.ClienteId).IsRequired();
                entity.Property(e => e.ProdutoId).IsRequired();
                entity.Property(e => e.DataCadastro).IsRequired();
                entity.HasOne(e => e.Produto).WithMany(e => e.Vendas).HasForeignKey(e => e.ProdutoId);
                entity.HasOne(e => e.Cliente).WithMany(e => e.Vendas).HasForeignKey(e => e.ClienteId);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
