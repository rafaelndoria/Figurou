namespace Figurou.Business.Models
{
    public class UsuarioDetalhe : Entidade
    {
        protected UsuarioDetalhe() { }

        public UsuarioDetalhe(
            string nome,
            string sobrenome,
            string estado,
            string cidade,
            string imagem)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Estado = estado;
            Cidade = cidade;
            Imagem = imagem;

            Reputacao = 0;
            Experiencia = 0;
            Nivel = 1;

            DataCriacao = DateTime.UtcNow;
        }

        public string Nome { get; private set; }
        public string Sobrenome { get; private set; }
        public string Estado { get; private set; }
        public string Cidade { get; private set; }
        public string Imagem { get; private set; }

        public int Reputacao { get; private set; }
        public int Experiencia { get; private set; }
        public int Nivel { get; private set; }

        public DateTime DataCriacao { get; private set; }

        public Usuario Usuario { get; private set; } = null!;
        public Guid UsuarioId { get; private set; }

        public void Atualizar(string nome, string sobrenome, string estado, string cidade, string imagem)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Estado = estado;
            Cidade = cidade;
            Imagem = imagem;
        }

        public void AdicionarExperiencia(int valor)
        {
            if (valor <= 0)
                return;

            Experiencia += valor;

            CalcularNivel();
        }

        public void AdicionarReputacao(int valor)
        {
            if (valor <= 0)
                return;

            Reputacao += valor;
        }

        public void RemoverReputacao(int valor)
        {
            if (valor <= 0)
                return;

            Reputacao -= valor;

            if (Reputacao < 0)
                Reputacao = 0;
        }

        private void CalcularNivel()
        {
            Nivel = (Experiencia / 100) + 1;
        }
    }
}