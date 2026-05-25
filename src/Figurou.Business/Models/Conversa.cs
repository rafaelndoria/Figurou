namespace Figurou.Business.Models
{
    public class Conversa : Entidade
    {
        protected Conversa() { }

        public Conversa(Guid trocaId)
        {
            TrocaId = trocaId;

            DataCriacao = DateTime.UtcNow;

            Mensagens = new List<Mensagem>();
        }

        public DateTime DataCriacao { get; private set; }

        public Troca Troca { get; private set; } = null!;
        public Guid TrocaId { get; private set; }

        public ICollection<Mensagem> Mensagens { get; private set; }
    }
}