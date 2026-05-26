using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class TrocaItemValidation : BaseValidation<TrocaItem>
    {
        public TrocaItemValidation()
        {
            ValidarObrigatorio(x => x.TrocaId);
            ValidarObrigatorio(x => x.FigurinhaId);
            ValidarObrigatorio(x => x.UsuarioId);

            RuleFor(x => x.TrocaId)
                .NotEqual(Guid.Empty)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.FigurinhaId)
                .NotEqual(Guid.Empty)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.UsuarioId)
                .NotEqual(Guid.Empty)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.Quantidade)
                .GreaterThanOrEqualTo(1)
                    .WithMessage(MensagemMaiorQueZero);
        }
    }
}