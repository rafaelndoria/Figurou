namespace Figurou.Business.Models
{
    public class FigurinhaUsuario : Entidade
    {
        protected FigurinhaUsuario() { }

        public FigurinhaUsuario(bool possuiNoAlbum = true)
        {
            PossuiNoAlbum = possuiNoAlbum;

            QuantidadeRepetida = 0;

            AtualizarDisponibilidadeTroca();
        }

        public bool PossuiNoAlbum { get; private set; }
        public int QuantidadeRepetida { get; private set; }
        public bool DisponivelTroca { get; private set; }

        public Usuario Usuario { get; private set; } = null!;
        public Guid UsuarioId { get; private set; }

        public Figurinha Figurinha { get; private set; } = null!;
        public Guid FigurinhaId { get; private set; }

        public void AdicionarRepetida()
        {
            QuantidadeRepetida++;

            AtualizarDisponibilidadeTroca();
        }

        public void RemoverRepetida()
        {
            if (QuantidadeRepetida <= 0)
                return;

            QuantidadeRepetida--;

            AtualizarDisponibilidadeTroca();
        }

        private void AtualizarDisponibilidadeTroca()
        {
            DisponivelTroca = QuantidadeRepetida > 0;
        }
    }
}