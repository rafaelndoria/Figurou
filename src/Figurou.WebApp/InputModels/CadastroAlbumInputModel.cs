namespace Figurou.WebApp.InputModels
{
    public class CadastroAlbumInputModel
    {
        public string Nome { get; set; }
        public int Ano { get; set; }
        public int TotalFigurinhas { get; set; }
        public string Descricao { get; set; }
        public IFormFile ImagemCapa { get; set; }
    }
}
