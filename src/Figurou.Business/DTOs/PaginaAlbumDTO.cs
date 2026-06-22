namespace Figurou.Business.DTOs
{
    public class PaginaAlbumDTO
    {
        public PaginaAlbumDTO(
            Guid id,
            int numeroPagina,
            string imagemPagina,
            decimal largura,
            decimal altura,
            Guid albumId,
            string nomeAlbum,
            Guid? selecaoId = null,
            string? codigoSelecao = null,
            string? nomeSelecao = null)
        {
            Id = id;
            NumeroPagina = numeroPagina;
            ImagemPagina = imagemPagina;
            Largura = largura;
            Altura = altura;
            AlbumId = albumId;
            NomeAlbum = nomeAlbum;
            SelecaoId = selecaoId;
            CodigoSelecao = codigoSelecao;
            NomeSelecao = nomeSelecao;
        }

        public Guid Id { get; private set; }
        public int NumeroPagina { get; private set; }
        public string ImagemPagina { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }

        public Guid AlbumId { get; private set; }
        public string NomeAlbum { get; private set; }
        public Guid? SelecaoId { get; set; }
        public string? CodigoSelecao { get; set; }
        public string? NomeSelecao { get; set; }
    }
}