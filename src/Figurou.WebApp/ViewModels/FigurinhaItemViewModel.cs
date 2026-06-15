using Figurou.Business.Enums;

namespace Figurou.WebApp.ViewModels
{
    public class FigurinhaItemViewModel
    {
        public string Codigo { get; set; }
        public int Numero { get; set; }
        public string NomeJogador { get; set; }
        public ETipoRaridade Raridade { get; set; }
        public Guid Id { get; set; }
        public Guid AlbumId { get; set; }
        public Guid PaginaId { get; set; }
    }
}
