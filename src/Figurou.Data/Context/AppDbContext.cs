using Figurou.Business.Models;

using Microsoft.EntityFrameworkCore;

namespace Figurou.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Album> Albuns { get; set; }
        public DbSet<Conversa> Conversas { get; set; }
        public DbSet<Figurinha> Figurinhas { get; set; }
        public DbSet<FigurinhaUsuario> FigurinhasUsuario { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
        public DbSet<PaginaAlbum> PaginasAlbum { get; set; }
        public DbSet<Selecao> Selecoes { get; set; }
        public DbSet<SlotPaginaAlbum> SlotsPaginaAlbum { get; set; }
        public DbSet<Troca> Trocas { get; set; }
        public DbSet<TrocaItem> TrocaItens { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioDetalhe> UsuarioDetalhes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties()
                    .Where(p => p.ClrType == typeof(string))))
            {
                property.SetColumnType("varchar(100)");
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}