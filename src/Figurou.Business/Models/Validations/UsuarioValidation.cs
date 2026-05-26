using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class UsuarioValidation : BaseValidation<Usuario>
    {
        public UsuarioValidation()
        {
            ValidarTexto(x => x.Username, 3, 50);

            RuleFor(x => x.Email)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .MaximumLength(150)
                    .WithMessage(MensagemTamanhoMaximo)
                .EmailAddress()
                    .WithMessage("O campo {PropertyName} deve ser um e-mail válido.");

            RuleFor(x => x.SenhaCodificada)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .MaximumLength(500)
                    .WithMessage(MensagemTamanhoMaximo);

            RuleFor(x => x.DataCriacao)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage("A data de criação não pode ser futura.");

            RuleFor(x => x.Username)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .MaximumLength(50)
                    .WithMessage(MensagemTamanhoMaximo);
        }
    }
}