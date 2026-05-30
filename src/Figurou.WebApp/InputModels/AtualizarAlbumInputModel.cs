namespace Figurou.WebApp.InputModels
{
    public class AtualizarAlbumInputModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public int Ano { get; set; }
        public int TotalFigurinhas { get; set; }
        public string Descricao { get; set; }
        public IFormFile? ImagemCapa { get; set; }
        public string Caminho { get; set; }
    }
}
