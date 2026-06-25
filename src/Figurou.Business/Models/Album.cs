namespace Figurou.Business.Models
{
    public class Album : Entidade
    {
        public Album() { }

        public Album(
            string nome,
            int ano,
            string descricao,
            string imagemCapa,
            int totalFigurinhas)
        {
            Nome = nome;
            Ano = ano;
            Descricao = descricao;
            ImagemCapa = imagemCapa;
            TotalFigurinhas = totalFigurinhas;

            Ativo = true;
            DataCriacao = DateTime.UtcNow;

            Paginas = new List<PaginaAlbum>();
            Figurinhas = new List<Figurinha>();
            Selecoes = new List<Selecao>();
            AlbunsEscolhidosUsuarios = new List<Usuario>();
        }

        public string Nome { get; private set; }
        public int Ano { get; private set; }
        public string Descricao { get; private set; }
        public string ImagemCapa { get; private set; }
        public int TotalFigurinhas { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public ICollection<PaginaAlbum> Paginas { get; private set; }
        public ICollection<Figurinha> Figurinhas { get; private set; }
        public ICollection<Selecao> Selecoes { get; private set; }
        public ICollection<Usuario> AlbunsEscolhidosUsuarios { get; private set; }

        public void Atualizar(string nome, int ano, string descricao, string imagemCapa, int totalFigurinhas)
        {
            Nome = nome;
            Ano = ano;
            Descricao = descricao;
            ImagemCapa = imagemCapa;
            TotalFigurinhas = totalFigurinhas;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }
}