namespace Figurou.Business.Models
{
    public class FigurinhaUsuario : Entidade
    {
        public FigurinhaUsuario() { }

        public FigurinhaUsuario(Guid figurinhaId, Guid usuarioId, int quantidade, bool possuiNoAlbum = true)
        {
            FigurinhaId = figurinhaId;
            UsuarioId = usuarioId;
            PossuiNoAlbum = possuiNoAlbum;

            Quantidade = quantidade;

            AtualizarDisponibilidadeTroca();
        }

        public bool PossuiNoAlbum { get; private set; }
        public int Quantidade { get; private set; }
        public bool DisponivelTroca { get; private set; }

        public Usuario Usuario { get; private set; } = null!;
        public Guid UsuarioId { get; private set; }

        public Figurinha Figurinha { get; private set; } = null!;
        public Guid FigurinhaId { get; private set; }

        public void AdicionarRepetida()
        {
            Quantidade++;

            AtualizarDisponibilidadeTroca();
        }

        public void RemoverRepetida()
        {
            if (Quantidade <= 0)
                return;

            Quantidade--;

            AtualizarDisponibilidadeTroca();
        }

        private void AtualizarDisponibilidadeTroca()
        {
            DisponivelTroca = Quantidade > 1;
        }

        public void Atualizar(int quantidade)
        {
            if (quantidade <= 0)
                return;
            Quantidade = quantidade;
            AtualizarDisponibilidadeTroca();
        }
    }
}