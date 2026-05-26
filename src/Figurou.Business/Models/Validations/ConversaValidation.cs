namespace Figurou.Business.Models.Validations
{
    public class ConversaValidation : BaseValidation<Conversa>
    {
        public ConversaValidation()
        {
            ValidarObrigatorio(x => x.DataCriacao);
        }
    }
}