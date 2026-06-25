namespace Figurou.Business.DTOs
{
    public class SalvarFigurinhaUsuarioDTO
    {
        public SalvarFigurinhaUsuarioDTO(Guid figurinhaId, int quantidade)
        {
            FigurinhaId = figurinhaId;
            Quantidade = quantidade;
        }

        public Guid FigurinhaId { get; private set; }
        public int Quantidade { get; private set; }
    }
}
