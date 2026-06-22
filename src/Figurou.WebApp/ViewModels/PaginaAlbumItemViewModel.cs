namespace Figurou.WebApp.ViewModels
{
    public class PaginaAlbumItemViewModel
    {
        public Guid Id { get; set; }
        public string ImagemPagina { get; set; }
        public int NumeroPagina { get; set; }
        public Guid AlbumId { get; set; }
        public Guid? SelecaoId { get; set; }
        public string? SelecaoNome { get; set; }
        public string? SelecaoCodigo { get; set; }
    }
}
