using Figurou.Business.DTOs;

namespace Figurou.Business.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDTO>> ObterTodos();
        Task<AlbumDTO> ObterPorId(Guid id);
        Task Cadastrar(SalvarAlbumDTO cadastroAlbumDTO);
        Task Atualizar(Guid id, SalvarAlbumDTO atualizarAlbumDTO);
        Task AlterarStatus(Guid id);
        Task<string?> AtualizarCapa(Guid id, Stream arquivo, string nomeArquivo);
        Task<string?> AdicionarCapa(Stream arquivo, string nomeArquivo);
    }
}
