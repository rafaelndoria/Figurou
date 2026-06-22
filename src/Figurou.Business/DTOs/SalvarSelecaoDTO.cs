namespace Figurou.Business.DTOs
{
    public class SalvarSelecaoDTO
    {
        public SalvarSelecaoDTO(string codigo, string nome, Guid albumId)
        {
            Codigo = codigo;
            Nome = nome;
            AlbumId = albumId;
        }

        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public Guid AlbumId { get; private set; }
    }
}
