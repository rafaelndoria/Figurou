namespace Figurou.WebApp.InputModels
{
    public class AtualizarSelecaoInputModel
    {
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
    }
}
