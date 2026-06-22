namespace Figurou.Business.DTOs
{
    public class SelecaoDTO
    {
        public SelecaoDTO(string codigo, string nome, string nomeAlbum, Guid id, Guid albumId)
        {
            Codigo = codigo;
            Nome = nome;
            NomeAlbum = nomeAlbum;
            Id = id;
            AlbumId = albumId;
        }

        public string Codigo { get; private set; }
        public string Nome { get; private set; }
        public string NomeAlbum { get; set; }
        public Guid Id { get; private set; }
        public Guid AlbumId { get; private set; }
    }
}
