namespace Figurou.WebApp.ViewModels
{
    public class AlbumViewModel
    {
        public string Nome { get; set; }
        public string Capa { get; set; }
        public string Descricao { get; set; }
        public int TotalFigurinhas { get; set; }
        public int Ano { get; set; }
        public bool Ativo { get; set; }
        public bool UsuarioSelecionado { get; set; }
        public Guid Id { get; set; }
    }
}

