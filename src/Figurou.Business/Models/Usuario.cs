using Figurou.Business.Enums;

namespace Figurou.Business.Models
{
    public class Usuario : Entidade
    {
        public Usuario() { }

        public Usuario(
            string userName,
            string email,
            string senhaCodificada)
        {
            Username = userName;
            Email = email.ToLower().Trim();
            SenhaCodificada = senhaCodificada;

            Ativo = true;
            DataCriacao = DateTime.UtcNow;
            Papel = EUsuarioRole.UsuarioPadrao;

            FigurinhasUsuario = new List<FigurinhaUsuario>();
            TrocasSolicitadas = new List<Troca>();
            TrocasRecebidas = new List<Troca>();
            TrocaItens = new List<TrocaItem>();
            MatchesSolicitados = new List<Match>();
            MatchesRecebidos = new List<Match>();
            Mensagens = new List<Mensagem>();
        }

        public string Username { get; private set; }
        public string Email { get; private set; }
        public string SenhaCodificada { get; private set; }
        public bool Ativo { get; private set; }
        public DateTime DataCriacao { get; private set; }
        public EUsuarioRole Papel { get; private set; }

        public Album Album { get; private set; } = null!;
        public Guid? AlbumEscolhidoId { get; private set; }

        public UsuarioDetalhe? UsuarioDetalhes { get; private set; }

        public ICollection<FigurinhaUsuario> FigurinhasUsuario { get; private set; }
        public ICollection<Troca> TrocasSolicitadas { get; private set; }
        public ICollection<Troca> TrocasRecebidas { get; private set; }
        public ICollection<TrocaItem> TrocaItens { get; private set; }
        public ICollection<Match> MatchesSolicitados { get; private set; }
        public ICollection<Match> MatchesRecebidos { get; private set; }
        public ICollection<Mensagem> Mensagens { get; private set; }

        public void AdicionarDetalhes(string nome, string sobrenome, string estado, string cidade, string imagem)
        {
            if (UsuarioDetalhes != null)
                return;

            UsuarioDetalhes = new UsuarioDetalhe(
                nome,
                sobrenome,
                estado,
                cidade,
                imagem);
        }

        public void AtualizarSenha(string senhaCodificada)
        {
            SenhaCodificada = senhaCodificada;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void EscolherAlbum(Guid id)
        {
            AlbumEscolhidoId = id;
        }

        public void RemoverAlbum()
        {
            AlbumEscolhidoId = null;
        }

        public void AdicionarFigurinha(FigurinhaUsuario figurinha)
        {
            FigurinhasUsuario.Add(figurinha);
        }
    }
}