using Microsoft.EntityFrameworkCore;
using ReciclaMaisAPI.Models;

namespace ReciclaMaisAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<ProdutoResiduo> ProdutosResiduos { get; set; }
        public DbSet<ItemColeta> ItensColeta { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemColeta>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemColeta>()
                .HasOne(i => i.Agendamento)
                .WithMany(a => a.ItensColeta)
                .HasForeignKey(i => i.AgendamentoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
