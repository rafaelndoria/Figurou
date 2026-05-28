using Figurou.Business.Enums;

namespace Figurou.Business.Services.Interfaces
{
    public interface IAuthService
    {
        string GerarJwtToken(string email, string nomeUsuario, EUsuarioRole papel, Guid usuarioId);
        string CriptografarSenha(string senha);
    }
}
