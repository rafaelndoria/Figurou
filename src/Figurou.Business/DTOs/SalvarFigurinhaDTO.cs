using Figurou.Business.Enums;

namespace Figurou.Business.DTOs
{
    public class SalvarFigurinhaDTO
    {
        public SalvarFigurinhaDTO(Guid paginaId, Guid albumId, string codigo, int numero, ETipoRaridade tipoRaridade, string nomeJogador)
        {
            PaginaId = paginaId;
            AlbumId = albumId;
            Codigo = codigo;
            Numero = numero;
            TipoRaridade = tipoRaridade;
            NomeJogador = nomeJogador;
        }

        public Guid PaginaId { get; private set; }
        public Guid AlbumId { get; private set; }
        public string Codigo { get; private set; }
        public int Numero { get; private set; }
        public ETipoRaridade TipoRaridade { get; private set; }
        public string NomeJogador { get; private set; }
    }
}
