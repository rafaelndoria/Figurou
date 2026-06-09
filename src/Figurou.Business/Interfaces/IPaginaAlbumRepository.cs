using Figurou.Business.Models;

namespace Figurou.Business.Interfaces
{
    public interface IPaginaAlbumRepository : IRepository<PaginaAlbum>
    {
        Task<PaginaAlbum> BuscarPaginaAlbumPorId(Guid id);
    }
}
