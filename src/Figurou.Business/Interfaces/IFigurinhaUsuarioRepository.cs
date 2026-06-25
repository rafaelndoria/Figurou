using Figurou.Business.Models;

namespace Figurou.Business.Interfaces
{
    public interface IFigurinhaUsuarioRepository : IRepository<FigurinhaUsuario>
    {
        void AdicionarSalvar(FigurinhaUsuario figurinhaUsuario);
    }
}
