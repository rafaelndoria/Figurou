using Figurou.Business.DTOs;

namespace Figurou.Business.Services.Interfaces
{
    public interface ISelecaoService
    {
        Task<IEnumerable<SelecaoDTO>> BuscarSelecoesAlbum(Guid albumId);
        Task Criar(SalvarSelecaoDTO salvarSelecaoDTO);
        Task Deletar(Guid id);
        Task Atualizar(Guid id, SalvarSelecaoDTO salvarSelecaoDTO);
    }
}
