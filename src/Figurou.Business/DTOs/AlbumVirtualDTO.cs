using Figurou.Business.Enums;

namespace Figurou.Business.DTOs
{
    public class AlbumVirtualDTO
    {
        public AlbumVirtualDTO(Guid albumId, string nomeAlbum, int totalFigurinhas, int totalPossuidas, int totalRepetidas, int totalFaltantes)
        {
            AlbumId = albumId;
            NomeAlbum = nomeAlbum;
            TotalFigurinhas = totalFigurinhas;
            TotalPossuidas = totalPossuidas;
            TotalRepetidas = totalRepetidas;
            TotalFaltantes = totalFaltantes;

            Grupos = new List<GrupoAlbumVirtualDTO>();
        }

        public Guid AlbumId { get; private set; }
        public string NomeAlbum { get; private set; }
        public int TotalFigurinhas { get; private set; }
        public int TotalPossuidas { get; private set; }
        public int TotalRepetidas { get; private set; }
        public int TotalFaltantes { get; private set; }
        public List<GrupoAlbumVirtualDTO> Grupos { get; private set; }

        public void AdicionarGrupo(GrupoAlbumVirtualDTO grupo)
        {
            Grupos.Add(grupo);
        }
    }

    public class GrupoAlbumVirtualDTO
    {
        public GrupoAlbumVirtualDTO(string nomeGrupo, int paginaInicial, int paginaFinal)
        {
            NomeGrupo = nomeGrupo;
            PaginaInicial = paginaInicial;
            PaginaFinal = paginaFinal;

            Figurinhas = new List<FigurinhaAlbumVirtualDTO>();
        }

        public string NomeGrupo { get; private set; }
        public int PaginaInicial { get; private set; }
        public int PaginaFinal { get; private set; }
        public List<FigurinhaAlbumVirtualDTO> Figurinhas { get; private set; }

        public void AdicionarFigurinha(FigurinhaAlbumVirtualDTO figurinha)
        {
            Figurinhas.Add(figurinha);
        }
    }

    public class FigurinhaAlbumVirtualDTO
    {
        public FigurinhaAlbumVirtualDTO(Guid figurinhaId, string codigo, ETipoRaridade raridade, int quantidade, int numero, string? nomeJogador = null)
        {
            FigurinhaId = figurinhaId;
            Codigo = codigo;
            Raridade = raridade;
            Quantidade = quantidade;
            NomeJogador = nomeJogador;
            Numero = numero;
        }

        public Guid FigurinhaId { get; private set; }
        public string Codigo { get; private set; }
        public ETipoRaridade Raridade { get; private set; }
        public int Quantidade { get; private set; }
        public int Numero { get; private set; }
        public string? NomeJogador { get; private set; }
    }
}
