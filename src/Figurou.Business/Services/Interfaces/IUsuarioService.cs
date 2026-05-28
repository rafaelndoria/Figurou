using Figurou.Business.Models;
using Figurou.WebApp.InputModel;

namespace Figurou.Business.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> Cadastrar(RegistroUsuarioInputModel registroUsuario);
        Task<Usuario?> Login(LoginUsuarioInputModel loginUsuario);
    }
}