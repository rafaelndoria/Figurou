namespace Figurou.Business.Models
{
    public class Match : Entidade
    {
        protected Match() { }

        public Match(
            int compatibilidade,
            Guid usuarioOrigemId,
            Guid usuarioDestinoId)
        {
            if (compatibilidade < 0)
                compatibilidade = 0;

            if (compatibilidade > 100)
                compatibilidade = 100;

            Compatibilidade = compatibilidade;

            UsuarioOrigemId = usuarioOrigemId;
            UsuarioDestinoId = usuarioDestinoId;
        }

        public int Compatibilidade { get; private set; }

        public Usuario UsuarioOrigem { get; private set; } = null!;
        public Guid UsuarioOrigemId { get; private set; }

        public Usuario UsuarioDestino { get; private set; } = null!;
        public Guid UsuarioDestinoId { get; private set; }
    }
}