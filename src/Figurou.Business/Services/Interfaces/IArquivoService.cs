namespace Figurou.Business.Services.Interfaces
{
    public interface IArquivoService
    {
        Task<string?> SalvarAsync(Stream arquivo, string nomeArquivo, string pasta);
        bool DeletarImagem(string caminho);
    }
}
