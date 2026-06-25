using Figurou.Business.DTOs;

namespace Figurou.Business.Services.Interfaces
{
    public interface IFigurinhaService
    {
        Task<IEnumerable<FigurinhaDTO>> BuscarFigurinhasPorPaginaAlbum(Guid paginaId, Guid albumId);
        Task<FigurinhaDTO?> ObterPorId(Guid id);
        Task Cadastrar(SalvarFigurinhaDTO figurinhaDTO);
        Task Deletar(Guid id);
        Task Atualizar(Guid id, SalvarFigurinhaDTO figurinhaDTO);
        Task AdicionarFigurinhaUsuario(Guid albumId, Guid usuarioId, IEnumerable<SalvarFigurinhaUsuarioDTO> figurinhas);
    }
}