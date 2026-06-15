using Figurou.Business.Models;

namespace Figurou.Business.Interfaces
{
    public interface IFigurinhaRepository : IRepository<Figurinha>
    {
        Task<PaginaAlbum> BuscarFigurasPorPagina(Guid paginaId, Guid albumId);
    }
}
