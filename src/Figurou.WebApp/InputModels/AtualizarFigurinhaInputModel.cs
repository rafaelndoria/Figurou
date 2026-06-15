using Figurou.Business.Enums;

namespace Figurou.WebApp.InputModels
{
    public class AtualizarFigurinhaInputModel
    {
        public string Codigo { get; set; }
        public int Numero { get; set; }
        public ETipoRaridade Raridade { get; set; }
        public string NomeJogador { get; set; }
        public Guid PaginaId { get; set; }
        public Guid AlbumId { get; set; }
        public Guid Id { get; set; }
    }
}
