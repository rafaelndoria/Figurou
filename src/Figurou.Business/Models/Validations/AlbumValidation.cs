using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class AlbumValidation : BaseValidation<Album>
    {
        public AlbumValidation()
        {
            ValidarTexto(x => x.Nome, 2, 100);

            RuleFor(x => x.Ano)
                .GreaterThan(1900)
                    .WithMessage("O campo {PropertyName} precisa ser maior que 1900.")
                .LessThanOrEqualTo(DateTime.UtcNow.Year + 1)
                    .WithMessage(MensagemValorInvalido);

            ValidarTexto(x => x.Descricao, 10, 250);

            ValidarImagem(x => x.ImagemCapa, 300);

            RuleFor(x => x.TotalFigurinhas)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero)
                .LessThanOrEqualTo(10000)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.DataCriacao)
                .LessThanOrEqualTo(DateTime.UtcNow)
                    .WithMessage("O campo {PropertyName} não pode ser uma data futura.");
        }
    }
}