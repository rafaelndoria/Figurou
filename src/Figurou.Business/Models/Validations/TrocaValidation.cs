using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class TrocaValidation : BaseValidation<Troca>
    {
        public TrocaValidation()
        {
            ValidarObrigatorio(x => x.SolicitanteId);
            ValidarObrigatorio(x => x.DestinatarioId);

            RuleFor(x => x.SolicitanteId)
                .NotEqual(x => x.DestinatarioId)
                    .WithMessage("O solicitante não pode ser o mesmo que o destinatário.");

            RuleFor(x => x.Observacao)
                .MaximumLength(200)
                    .WithMessage(MensagemTamanhoMaximo);

            RuleFor(x => x.DataRequisicao)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage("A data de requisição não pode ser futura.");

            RuleFor(x => x.DataFinalizacao)
                .GreaterThanOrEqualTo(x => x.DataRequisicao)
                    .When(x => x.DataFinalizacao.HasValue)
                    .WithMessage("A data de finalização não pode ser menor que a data de requisição.");

            RuleFor(x => x.Status)
                .IsInEnum()
                    .WithMessage(MensagemValorInvalido);
        }
    }
}