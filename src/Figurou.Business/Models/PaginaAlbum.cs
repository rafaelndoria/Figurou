namespace Figurou.Business.Models
{
    public class PaginaAlbum : Entidade
    {
        protected PaginaAlbum() { }

        public PaginaAlbum(
            int numeroPagina,
            string imagemPagina,
            decimal largura,
            decimal altura)
        {
            NumeroPagina = numeroPagina;
            ImagemPagina = imagemPagina;
            Largura = largura;
            Altura = altura;

            Figurinhas = new List<Figurinha>();
            SlotsPaginaAlbum = new List<SlotsPaginaAlbum>();
        }

        public int NumeroPagina { get; private set; }
        public string ImagemPagina { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }

        public Album Album { get; private set; } = null!;
        public Guid AlbumId { get; private set; }
        public ICollection<Figurinha> Figurinhas { get; private set; }
        public ICollection<SlotsPaginaAlbum> SlotsPaginaAlbum { get; private set; }

        public void Atualizar(int numeroPagina, string imagemPagina, decimal largura, decimal altura)
        {
            NumeroPagina = numeroPagina;
            ImagemPagina = imagemPagina;
            Largura = largura;
            Altura = altura;
        }
    }
}