using Microsoft.EntityFrameworkCore;
using ReciclaMais.API.Models;

namespace ReciclaMais.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProdutoResiduo> ProdutosResiduos { get; set; }
        public DbSet<ItemColeta> ItensColeta { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<Noticia> Noticias { get; set; }
        public DbSet<Beneficio> Beneficios { get; set; }
        public DbSet<FaleConosco> FaleConoscos { get; set; } // ✅ Corrigido: singular e consistente com EF Core

        // Usuários e especializações
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Municipe> Municipes { get; set; }
        public DbSet<OrgaoPublico> OrgaosPublicos { get; set; }
        public DbSet<Administrador> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ItemColeta → Produto
            modelBuilder.Entity<ItemColeta>()
                .HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .OnDelete(DeleteBehavior.Restrict);

            // ItemColeta → Agendamento
            modelBuilder.Entity<ItemColeta>()
                .HasOne(i => i.Agendamento)
                .WithMany(a => a.ItensColeta)
                .HasForeignKey(i => i.AgendamentoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento: Usuario → Municipe
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Municipe)
                .WithOne(m => m.Usuario)
                .HasForeignKey<Municipe>(m => m.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento: Usuario → OrgaoPublico
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.OrgaoPublico)
                .WithOne(o => o.Usuario)
                .HasForeignKey<OrgaoPublico>(o => o.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relacionamento: Usuario → Administrador
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Administrador)
                .WithOne(a => a.Usuario)
                .HasForeignKey<Administrador>(a => a.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);

            // Enum: Usuario.Tipo como string
            modelBuilder.Entity<Usuario>()
                .Property(u => u.Tipo)
                .HasConversion<string>();

            // 🔒 Forçar nome exato da tabela no banco
            modelBuilder.Entity<FaleConosco>().ToTable("FaleConosco");
        }
    }
}
