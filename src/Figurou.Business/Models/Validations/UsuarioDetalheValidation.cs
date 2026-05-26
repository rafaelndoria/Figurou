using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class UsuarioDetalheValidation : BaseValidation<UsuarioDetalhe>
    {
        public UsuarioDetalheValidation()
        {
            ValidarTexto(x => x.Nome, 2, 100);

            ValidarTexto(x => x.Sobrenome, 2, 100);

            RuleFor(x => x.Estado)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .Length(2)
                    .WithMessage("O campo {PropertyName} deve conter 2 caracteres.");

            RuleFor(x => x.Cidade)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .MaximumLength(100)
                    .WithMessage(MensagemTamanhoMaximo);

            ValidarImagem(x => x.Imagem, 150);

            RuleFor(x => x.UsuarioId)
                .NotEqual(Guid.Empty)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.Reputacao)
                .GreaterThanOrEqualTo(0)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.Experiencia)
                .GreaterThanOrEqualTo(0)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.Nivel)
                .GreaterThanOrEqualTo(1)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.DataCriacao)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage("A data de criação não pode ser futura.");
        }
    }
}