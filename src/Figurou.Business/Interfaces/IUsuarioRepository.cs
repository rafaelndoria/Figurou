using Figurou.Business.Models;

namespace Figurou.Business.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> BuscarUsuarioPorUsername(string userName);
    }
}
