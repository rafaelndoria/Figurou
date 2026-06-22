namespace Figurou.Business.Models
{
    public class Selecao : Entidade
    {
        public Selecao() { }

        public Selecao(
            string codigo,
            string nome,
            Guid albumId)
        {
            Codigo = codigo;
            Nome = nome;
            AlbumId = albumId;

            PaginasAlbum = new List<PaginaAlbum>();
        }

        public string Codigo { get; private set; }
        public string Nome { get; private set; }

        public Album Album { get; private set; } = null!;
        public Guid AlbumId { get; private set; }
        public IEnumerable<PaginaAlbum> PaginasAlbum { get; private set; }

        public void Atualizar(string codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }
    }
}