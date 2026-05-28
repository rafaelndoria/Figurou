using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Figurou.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext db) : base(db) { }

        public async Task<Usuario> BuscarUsuarioPorUsername(string userName)
        {
            return await Db.Usuarios.FirstOrDefaultAsync(x => x.Username == userName || x.Email == userName);
        }
    }
}
