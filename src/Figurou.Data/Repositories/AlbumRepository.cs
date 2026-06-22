using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Figurou.Data.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Album?> BuscarPaginasAlbum(Guid id)
        {
            return await Db.Albuns
                .AsNoTracking()
                .Include(x => x.Paginas)
                .ThenInclude(x => x.Selecao)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}