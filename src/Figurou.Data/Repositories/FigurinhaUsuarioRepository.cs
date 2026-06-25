using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

namespace Figurou.Data.Repositories
{
    public class FigurinhaUsuarioRepository : Repository<FigurinhaUsuario>, IFigurinhaUsuarioRepository
    {
        public FigurinhaUsuarioRepository(AppDbContext db) : base(db)
        {
        }

        public void AdicionarSalvar(FigurinhaUsuario figurinhaUsuario)
        {
            Db.FigurinhasUsuario.Add(figurinhaUsuario);
        }
    }
}
