using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class PaginaAlbumValidation : BaseValidation<PaginaAlbum>
    {
        public PaginaAlbumValidation()
        {
            RuleFor(x => x.NumeroPagina)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero);

            ValidarImagem(x => x.ImagemPagina, 500);

            RuleFor(x => x.Largura)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero);

            RuleFor(x => x.Altura)
                .GreaterThan(0)
                    .WithMessage(MensagemMaiorQueZero);

            ValidarObrigatorio(x => x.AlbumId);
        }
    }
}