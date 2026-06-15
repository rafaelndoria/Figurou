using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

using Microsoft.EntityFrameworkCore;

namespace Figurou.Data.Repositories
{
    public class FigurinhaRepository : Repository<Figurinha>, IFigurinhaRepository
    {
        public FigurinhaRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<PaginaAlbum> BuscarFigurasPorPagina(Guid paginaId, Guid albumId)
        {
            return await Db.PaginasAlbum.Include(x => x.Figurinhas).Include(x => x.Album).Where(x => x.AlbumId == albumId && x.Id == paginaId).FirstOrDefaultAsync();
        }
    }
}
