namespace Figurou.Business.Models
{
    public class PaginaAlbum : Entidade
    {
        public PaginaAlbum() { }

        public PaginaAlbum(
            int numeroPagina,
            string imagemPagina,
            decimal largura,
            decimal altura,
            Guid albumId)
        {
            NumeroPagina = numeroPagina;
            ImagemPagina = imagemPagina;
            Largura = largura;
            Altura = altura;
            AlbumId = albumId;

            Figurinhas = new List<Figurinha>();
            SlotsPaginaAlbum = new List<SlotPaginaAlbum>();
        }

        public int NumeroPagina { get; private set; }
        public string ImagemPagina { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }

        public Album Album { get; private set; } = null!;
        public Guid AlbumId { get; private set; }
        public ICollection<Figurinha> Figurinhas { get; private set; }
        public ICollection<SlotPaginaAlbum> SlotsPaginaAlbum { get; private set; }

        public void Atualizar(int numeroPagina, string imagemPagina, decimal largura, decimal altura)
        {
            NumeroPagina = numeroPagina;
            ImagemPagina = imagemPagina;
            Largura = largura;
            Altura = altura;
        }
    }
}