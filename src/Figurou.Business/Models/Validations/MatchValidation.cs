using FluentValidation;

namespace Figurou.Business.Models.Validations
{
    public class MatchValidation : BaseValidation<Match>
    {
        public MatchValidation()
        {
            RuleFor(x => x.Compatibilidade)
                .InclusiveBetween(0, 100)
                    .WithMessage("O campo {PropertyName} precisa estar entre 0 e 100.");

            ValidarObrigatorio(x => x.UsuarioOrigemId);

            ValidarObrigatorio(x => x.UsuarioDestinoId);

            RuleFor(x => x)
                .Must(UsuariosDiferentes)
                    .WithMessage("O usuário de origem e destino não podem ser iguais.");
        }

        private static bool UsuariosDiferentes(Match match)
        {
            return match.UsuarioOrigemId != match.UsuarioDestinoId;
        }
    }
}