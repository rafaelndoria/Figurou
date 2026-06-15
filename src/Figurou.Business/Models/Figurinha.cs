using Figurou.Business.Enums;

namespace Figurou.Business.Models
{
    public class Figurinha : Entidade
    {
        public Figurinha() { }

        public Figurinha(
            string codigo,
            int numero,
            ETipoRaridade raridade,
            Guid albumId,
            Guid paginaAlbumId,
            string? nomeJogador = "")
        {
            Codigo = codigo;
            Numero = numero;
            Raridade = raridade;
            AlbumId = albumId;
            PaginaAlbumId = paginaAlbumId;
            NomeJogador = nomeJogador;

            SlotsPaginaAlbum = new List<SlotPaginaAlbum>();
            FigurinhasUsuario = new List<FigurinhaUsuario>();
            TrocaItens = new List<TrocaItem>();
        }

        public string Codigo { get; private set; }
        public int Numero { get; private set; }
        public string? NomeJogador { get; private set; }
        public ETipoRaridade Raridade { get; private set; }

        public Album Album { get; private set; } = null!;
        public Guid AlbumId { get; private set; }

        public PaginaAlbum PaginaAlbum { get; private set; } = null!;
        public Guid PaginaAlbumId { get; private set; }

        public ICollection<SlotPaginaAlbum> SlotsPaginaAlbum { get; private set; }
        public ICollection<FigurinhaUsuario> FigurinhasUsuario { get; private set; }
        public ICollection<TrocaItem> TrocaItens { get; private set; }

        public void Atualizar(string codigo, int numero, ETipoRaridade raridade, string nomeJogador)
        {
            Codigo = codigo;
            Numero = numero;
            Raridade = raridade;
            NomeJogador = nomeJogador;
        }
    }
}