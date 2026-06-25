using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Figurou.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(AppDbContext db) : base(db) { }

        public async Task<Usuario> BuscarUsuarioCompleto(Guid id)
        {
            return await Db.Usuarios
                .Include(x => x.Album)
                    .ThenInclude(x => x.Paginas)
                        .ThenInclude(x => x.Figurinhas)
                .Include(x => x.Album)
                    .ThenInclude(x => x.Selecoes)
                .Include(x => x.FigurinhasUsuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Usuario> BuscarUsuarioFigurinhasColetadas(Guid usuarioId)
        {
            return await Db.Usuarios.Include(x => x.FigurinhasUsuario).FirstOrDefaultAsync(x => x.Id == usuarioId);
        }

        public async Task<Usuario> BuscarUsuarioPorUsername(string userName)
        {
            return await Db.Usuarios.FirstOrDefaultAsync(x => x.Username == userName || x.Email == userName);
        }
    }
}
