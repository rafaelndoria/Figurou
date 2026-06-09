using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Figurou.Data.Repositories
{
    public class PaginaAlbumRepository : Repository<PaginaAlbum>, IPaginaAlbumRepository
    {
        public PaginaAlbumRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<PaginaAlbum> BuscarPaginaAlbumPorId(Guid id)
        {
            return await Db.PaginasAlbum.Include(x => x.Album).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
