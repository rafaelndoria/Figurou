using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class MensagemValidation : BaseValidation<Mensagem>
    {
        public MensagemValidation()
        {
            ValidarTexto(x => x.Conteudo, 1, 1000);

            RuleFor(x => x.UsuarioId)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio);

            RuleFor(x => x.ConversaId)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio);

            RuleFor(x => x.DataEnvio)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage("O campo {PropertyName} não pode ser uma data futura.");

            RuleFor(x => x)
                .Must(MensagemEditadaValida)
                    .WithMessage("Uma mensagem excluída não pode ser editada.");
        }

        private static bool MensagemEditadaValida(Mensagem mensagem)
        {
            if (mensagem.Excluida && mensagem.Editada)
                return false;

            return true;
        }
    }
}