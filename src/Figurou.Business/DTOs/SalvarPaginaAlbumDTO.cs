namespace Figurou.Business.DTOs
{
    public class SalvarPaginaAlbumDTO
    {
        public SalvarPaginaAlbumDTO(
            Guid id,
            int numeroPagina,
            string imagemPagina,
            decimal largura,
            decimal altura,
            Guid albumId,
            Guid? selecaoId = null)
        {
            Id = id;
            NumeroPagina = numeroPagina;
            ImagemPagina = imagemPagina;
            Largura = largura;
            Altura = altura;
            AlbumId = albumId;
            SelecaoId = selecaoId;
        }

        public Guid Id { get; private set; }
        public int NumeroPagina { get; private set; }
        public string ImagemPagina { get; private set; }
        public decimal Largura { get; private set; }
        public decimal Altura { get; private set; }
        public Guid AlbumId { get; private set; }
        public Guid? SelecaoId { get; set; }
    }
}