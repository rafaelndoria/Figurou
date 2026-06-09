namespace Figurou.WebApp.ViewModels
{
    public class PaginaAlbumViewModel
    {
        public string NomeAlbum { get; set; }
        public Guid AlbumId { get; set; }
        public IEnumerable<PaginaAlbumItemViewModel> Paginas { get; set; }
    }
}
