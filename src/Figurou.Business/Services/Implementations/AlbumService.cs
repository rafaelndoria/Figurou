using Figurou.Business.DTOs;
using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Business.Services.Interfaces;

namespace Figurou.Business.Services.Implementations
{
    public class AlbumService : BaseService, IAlbumService
    {
        const string NomePastaImagem = "uploads/albuns";

        private readonly IAlbumRepository _albumRepository;
        private readonly IArquivoService _arquivoService;

        public AlbumService(
            INotificador notificador,
            IAlbumRepository albumRepository,
            IArquivoService arquivoService) : base(notificador)
        {
            _albumRepository = albumRepository;
            _arquivoService = arquivoService;
        }

        public async Task Atualizar(Guid id, SalvarAlbumDTO atualizarAlbumDTO)
        {
            var album = await _albumRepository.ObterPorIdAsync(id);

            if (album == null)
            {
                Notificar("Álbum não encontrado.");
                return;
            }

            album.Atualizar(
                atualizarAlbumDTO.Nome,
                atualizarAlbumDTO.Ano,
                atualizarAlbumDTO.Descricao,
                atualizarAlbumDTO.ImagemCapa,
                atualizarAlbumDTO.TotalFigurinhas);

            await _albumRepository.AtualizarAsync(album);
        }

        public async Task<string?> AdicionarCapa(Stream arquivo, string nomeArquivo)
        {
            if (arquivo == null || arquivo.Length == 0)
            {
                Notificar("Arquivo inválido.");
                return null;
            }

            return await _arquivoService.SalvarAsync(arquivo, nomeArquivo, NomePastaImagem);
        }

        public async Task<string?> AtualizarCapa(Guid id, Stream arquivo, string nomeArquivo)
        {
            var album = await _albumRepository.ObterPorIdAsync(id);

            if (album == null)
            {
                Notificar("Álbum não encontrado.");
                return null;
            }

            if (!string.IsNullOrWhiteSpace(album.ImagemCapa))
            {
                _arquivoService.DeletarImagem(album.ImagemCapa);
            }

            return await _arquivoService.SalvarAsync(arquivo, nomeArquivo, NomePastaImagem);
        }

        public async Task Cadastrar(SalvarAlbumDTO cadastroAlbumDTO)
        {
            var album = new Album(
                cadastroAlbumDTO.Nome,
                cadastroAlbumDTO.Ano,
                cadastroAlbumDTO.Descricao,
                cadastroAlbumDTO.ImagemCapa,
                cadastroAlbumDTO.TotalFigurinhas);

            await _albumRepository.AdicionarAsync(album);
        }

        public async Task<AlbumDTO> ObterPorId(Guid id)
        {
            var album = await _albumRepository.ObterPorIdAsync(id);

            if (album == null)
            {
                Notificar("Álbum não encontrado.");
                return null;
            }

            return new AlbumDTO(
                album.Id,
                album.Nome,
                album.Ano,
                album.Descricao,
                album.ImagemCapa,
                album.TotalFigurinhas,
                album.Ativo,
                album.DataCriacao);
        }

        public async Task<IEnumerable<AlbumDTO>> ObterTodos()
        {
            var albuns = await _albumRepository.ObterTodosAsync();

            return albuns.Select(album => new AlbumDTO(
                album.Id,
                album.Nome,
                album.Ano,
                album.Descricao,
                album.ImagemCapa,
                album.TotalFigurinhas,
                album.Ativo,
                album.DataCriacao));
        }

        public async Task AlterarStatus(Guid id)
        {
            var album = await _albumRepository.ObterPorIdAsync(id);

            if (album == null)
            {
                Notificar("Álbum não encontrado.");
                return;
            }

            if (album.Ativo)
                album.Desativar();
            else
                album.Ativar();

            await _albumRepository.AtualizarAsync(album);
        }
    }
}