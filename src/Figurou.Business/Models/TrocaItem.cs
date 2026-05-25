namespace Figurou.Business.Models
{
    public class TrocaItem : Entidade
    {
        protected TrocaItem() { }

        public TrocaItem(
            Guid trocaId,
            Guid figurinhaId,
            Guid usuarioId)
        {
            TrocaId = trocaId;
            FigurinhaId = figurinhaId;
            UsuarioId = usuarioId;

            Quantidade = 1;
        }

        public int Quantidade { get; private set; }

        public Troca Troca { get; private set; } = null!;
        public Guid TrocaId { get; private set; }

        public Figurinha Figurinha { get; private set; } = null!;
        public Guid FigurinhaId { get; private set; }

        public Usuario Usuario { get; private set; } = null!;
        public Guid UsuarioId { get; private set; }

        public void AdicionarQuantidade()
        {
            Quantidade++;
        }

        public void RemoverQuantidade()
        {
            if (Quantidade <= 1)
                return;

            Quantidade--;
        }
    }
}