namespace Figurou.WebApp.ViewModels
{
    public class FigurinhaViewModel
    {
        public Guid PaginaId { get; set; }
        public Guid AlbumId { get; set; }
        public string NomeAlbum { get; set; }
        public int NumeroPagina { get; set; }
        public IEnumerable<FigurinhaItemViewModel> Figurinhas { get; set; }
    }
}
