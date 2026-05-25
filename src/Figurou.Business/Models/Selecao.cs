namespace Figurou.Business.Models
{
    public class Selecao : Entidade
    {
        protected Selecao() { }

        public Selecao(
            string codigo,
            string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }

        public string Codigo { get; private set; }
        public string Nome { get; private set; }

        public Album Album { get; private set; } = null!;
        public Guid AlbumId { get; private set; }

        public void Atualizar(string codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
        }
    }
}