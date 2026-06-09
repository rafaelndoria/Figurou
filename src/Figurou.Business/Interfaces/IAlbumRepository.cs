using Figurou.Business.Models;

namespace Figurou.Business.Interfaces
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<Album?> BuscarPaginasAlbum(Guid id);
    }
}
