using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class SelecaoValidation : BaseValidation<Selecao>
    {
        public SelecaoValidation()
        {
            ValidarTexto(x => x.Nome, 2, 50);

            RuleFor(x => x.Codigo)
                .NotEmpty()
                    .WithMessage(MensagemCampoObrigatorio)
                .Length(3, 3)
                    .WithMessage("O campo {PropertyName} precisa conter exatamente 3 caracteres.")
                .Matches("^[A-Z]{3}$")
                    .WithMessage("O campo {PropertyName} precisa conter apenas letras maiúsculas.");

            ValidarObrigatorio(x => x.AlbumId);
        }
    }
}