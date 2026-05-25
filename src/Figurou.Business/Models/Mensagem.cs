namespace Figurou.Business.Models
{
    public class Mensagem : Entidade
    {
        protected Mensagem() { }

        public Mensagem(
            string conteudo,
            Guid conversaId,
            Guid usuarioId)
        {
            Conteudo = conteudo;
            ConversaId = conversaId;
            UsuarioId = usuarioId;

            DataEnvio = DateTime.UtcNow;

            Lida = false;
            Editada = false;
            Excluida = false;
        }

        public string Conteudo { get; private set; }
        public bool Lida { get; private set; }
        public bool Editada { get; private set; }
        public bool Excluida { get; private set; }
        public DateTime DataEnvio { get; private set; }

        public Usuario Usuario { get; private set; } = null!;
        public Guid UsuarioId { get; private set; }

        public Conversa Conversa { get; private set; } = null!;
        public Guid ConversaId { get; private set; }

        public bool PodeEditarDeletar()
        {
            return DateTime.UtcNow <= DataEnvio.AddHours(2);
        }

        public void LerMensagem()
        {
            Lida = true;
        }

        public void EditarMensagem(string conteudo)
        {
            if (!PodeEditarDeletar())
                return;

            Conteudo = conteudo;
            Editada = true;
        }

        public void ExcluirMensagem()
        {
            if (!PodeEditarDeletar())
                return;

            Excluida = true;
        }
    }
}