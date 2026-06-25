using Figurou.Business.Enums;

namespace Figurou.WebApp.ViewModels
{
    public class AlbumVirtualViewModel
    {
        public Guid AlbumId { get; set; }
        public string NomeAlbum { get; set; } = string.Empty;
        public int TotalFigurinhas { get; set; }
        public int TotalPossuidas { get; set; }
        public int TotalRepetidas { get; set; }
        public int TotalFaltantes { get; set; }
        public IEnumerable<GrupoAlbumVirtualViewModel> Grupos { get; set; } = [];
    }

    public class GrupoAlbumVirtualViewModel
    {
        public string NomeGrupo { get; set; } = string.Empty;
        public int PaginaInicial { get; set; }
        public int PaginaFinal { get; set; }
        public IEnumerable<FigurinhaAlbumVirtualViewModel> Figurinhas { get; set; } = [];
    }

    public class FigurinhaAlbumVirtualViewModel
    {
        public Guid FigurinhaId { get; set; }
        public string Codigo { get; set; } = string.Empty;
        public ETipoRaridade Raridade { get; set; }
        public string? NomeJogador { get; set; } = string.Empty;
        public int Quantidade { get; set; }
    }

}
