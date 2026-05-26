using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class SlotPaginaAlbumValidation : BaseValidation<SlotPaginaAlbum>
    {
        public SlotPaginaAlbumValidation()
        {
            RuleFor(x => x.PosicaoX)
                .GreaterThanOrEqualTo(0)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.PosicaoY)
                .GreaterThanOrEqualTo(0)
                    .WithMessage(MensagemValorInvalido);

            RuleFor(x => x.Largura)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero);

            RuleFor(x => x.Altura)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero);

            RuleFor(x => x.Ordem)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero);

            ValidarObrigatorio(x => x.PaginaAlbumId);

            ValidarObrigatorio(x => x.FigurinhaId);
        }
    }
}