using Figurou.Business.DTOs;

namespace Figurou.Business.Services.Interfaces
{
    public interface IPaginaAlbumService
    {
        Task<IEnumerable<PaginaAlbumDTO>> BuscarPaginasPorAlbum(Guid albumId);
        Task<PaginaAlbumDTO?> ObterPaginaPorId(Guid id);
        Task Adicionar(SalvarPaginaAlbumDTO paginaAlbum);
        Task<bool> ExistePagina(Guid albumId, int numeroPagina);
        Task Excluir(Guid id);
        Task Atualizar(Guid id, SalvarPaginaAlbumDTO paginaAlbum);
    }
}
