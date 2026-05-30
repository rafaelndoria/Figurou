using Figurou.Business.DTOs;
using Figurou.Business.Models;

namespace Figurou.Business.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<Usuario?> Cadastrar(CadastrarUsuarioDTO registroUsuario);
        Task<Usuario?> Login(LoginUsuarioDTO loginUsuario);
    }
}