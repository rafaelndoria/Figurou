using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class FigurinhaValidation : BaseValidation<Figurinha>
    {
        public FigurinhaValidation()
        {
            ValidarTexto(x => x.Codigo, 1, 20);

            RuleFor(x => x.Numero)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero);

            RuleFor(x => x.Raridade)
                .IsInEnum()
                    .WithMessage(MensagemValorInvalido);

            ValidarObrigatorio(x => x.AlbumId);

            ValidarObrigatorio(x => x.PaginaAlbumId);
        }
    }
}