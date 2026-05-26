using FluentValidation;

using System.Linq.Expressions;

namespace Figurou.Business.Models.Validations
{
    public abstract class BaseValidation<T> : AbstractValidator<T> where T : Entidade
    {
        protected const string MensagemCampoObrigatorio =
            "O campo {PropertyName} precisa ser fornecido.";

        protected const string MensagemIntervaloCaracteres =
            "O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres.";

        protected const string MensagemTamanhoMaximo =
            "O campo {PropertyName} precisa ter no máximo {MaxLength} caracteres.";

        protected const string MensagemValorInvalido =
            "O campo {PropertyName} está inválido.";

        protected const string MensagemMaiorQueZero =
            "O campo {PropertyName} precisa ser maior que zero.";

        protected void ValidarTexto(
            Expression<Func<T, string>> propriedade,
            int minimo,
            int maximo)
        {
            RuleFor(propriedade)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .Length(minimo, maximo)
                    .WithMessage(MensagemIntervaloCaracteres);
        }

        protected void ValidarObrigatorio<TProperty>(
            Expression<Func<T, TProperty>> propriedade)
        {
            RuleFor(propriedade)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio);
        }

        protected void ValidarImagem(Expression<Func<T, string>> propriedade, int tamanhoMaximo)
        {
            RuleFor(propriedade)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .MaximumLength(tamanhoMaximo)
                    .WithMessage(MensagemTamanhoMaximo)
                .Must(ImagemValida)
                    .WithMessage("O campo {PropertyName} precisa conter uma imagem válida.");
        }

        private static bool ImagemValida(string imagem)
        {
            if (string.IsNullOrWhiteSpace(imagem))
                return false;

            string[] extensoesValidas =
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".webp"
            };

            return extensoesValidas.Any(ext =>
                imagem.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
        }
    }
}