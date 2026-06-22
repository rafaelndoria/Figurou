namespace Figurou.WebApp.ViewModels
{
    public class SelecaoListaViewModel
    {
        public Guid AlbumId { get; set; }
        public string NomeAlbum { get; set; } = string.Empty;
        public List<SelecaoViewModel> Selecoes { get; set; } = [];
    }
}