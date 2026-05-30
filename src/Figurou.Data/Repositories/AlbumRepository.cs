using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

namespace Figurou.Data.Repositories
{
    public class AlbumRepository : Repository<Album>, IAlbumRepository
    {
        public AlbumRepository(AppDbContext db) : base(db)
        {
        }
    }
}
