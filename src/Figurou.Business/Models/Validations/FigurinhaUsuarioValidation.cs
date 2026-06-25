using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class FigurinhaUsuarioValidation : BaseValidation<FigurinhaUsuario>
    {
        public FigurinhaUsuarioValidation()
        {
            RuleFor(x => x.Quantidade)
                .GreaterThanOrEqualTo(0)
                    .WithMessage("O campo {PropertyName} não pode ser negativo.");

            ValidarObrigatorio(x => x.UsuarioId);

            ValidarObrigatorio(x => x.FigurinhaId);

            RuleFor(x => x)
                .Must(ValidarDisponibilidadeTroca)
                    .WithMessage("Disponibilidade de troca inválida.");
        }

        private static bool ValidarDisponibilidadeTroca(FigurinhaUsuario figurinhaUsuario)
        {
            return figurinhaUsuario.DisponivelTroca ==
                   (figurinhaUsuario.Quantidade > 0);
        }
    }
}