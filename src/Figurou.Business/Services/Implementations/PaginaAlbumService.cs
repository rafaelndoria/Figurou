using Figurou.Business.DTOs;
using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Business.Services.Interfaces;

namespace Figurou.Business.Services.Implementations
{
    public class PaginaAlbumService : BaseService, IPaginaAlbumService
    {
        private readonly IAlbumRepository _albumRepository;
        private readonly IPaginaAlbumRepository _paginaAlbumRepository;

        public PaginaAlbumService(
            INotificador notificador,
            IAlbumRepository albumRepository,
            IPaginaAlbumRepository paginaAlbumRepository) : base(notificador)
        {
            _albumRepository = albumRepository;
            _paginaAlbumRepository = paginaAlbumRepository;
        }

        public async Task<bool> ExistePagina(Guid albumId, int numeroPagina)
        {
            if (numeroPagina <= 0) return false;

            var paginasExistentes = await _paginaAlbumRepository.BuscarAsync(
                x => x.AlbumId == albumId &&
                     x.NumeroPagina == numeroPagina);

            return paginasExistentes.Any();
        }

        public async Task Adicionar(SalvarPaginaAlbumDTO paginaAlbumDTO)
        {
            if (paginaAlbumDTO.NumeroPagina <= 0)
            {
                Notificar("Número da página inválido.");
                return;
            }

            if (paginaAlbumDTO.Largura <= 0)
            {
                Notificar("Largura inválida.");
                return;
            }

            if (paginaAlbumDTO.Altura <= 0)
            {
                Notificar("Altura inválida.");
                return;
            }

            var album = await _albumRepository.ObterPorIdAsync(paginaAlbumDTO.AlbumId);
            if (album == null)
            {
                Notificar("Álbum não encontrado.");
                return;
            }

            if (await ExistePagina(paginaAlbumDTO.AlbumId, paginaAlbumDTO.NumeroPagina))
            {
                Notificar("Já existe uma página com esse número cadastrado.");
                return;
            }

            var paginaAlbum = new PaginaAlbum(
                paginaAlbumDTO.NumeroPagina,
                paginaAlbumDTO.ImagemPagina,
                paginaAlbumDTO.Largura,
                paginaAlbumDTO.Altura,
                paginaAlbumDTO.AlbumId);

            await _paginaAlbumRepository.AdicionarAsync(paginaAlbum);
        }

        public async Task<IEnumerable<PaginaAlbumDTO>> BuscarPaginasPorAlbum(Guid albumId)
        {
            var album = await _albumRepository.BuscarPaginasAlbum(albumId);

            if (album == null)
            {
                Notificar("Álbum não encontrado.");
                return Enumerable.Empty<PaginaAlbumDTO>();
            }

            if (album.Paginas == null || !album.Paginas.Any())
                return Enumerable.Empty<PaginaAlbumDTO>();

            return album.Paginas
                .OrderBy(x => x.NumeroPagina)
                .Select(x => new PaginaAlbumDTO(
                    x.Id,
                    x.NumeroPagina,
                    x.ImagemPagina,
                    x.Largura,
                    x.Altura,
                    x.Album.Id,
                    x.Album.Nome
                ));
        }

        public async Task<PaginaAlbumDTO?> ObterPaginaPorId(Guid id)
        {
            var paginaAlbum = await _paginaAlbumRepository.BuscarPaginaAlbumPorId(id);

            if (paginaAlbum == null)
            {
                Notificar("Página não encontrada.");
                return null;
            }

            return new PaginaAlbumDTO(
                paginaAlbum.Id,
                paginaAlbum.NumeroPagina,
                paginaAlbum.ImagemPagina,
                paginaAlbum.Largura,
                paginaAlbum.Altura,
                paginaAlbum.Album.Id,
                paginaAlbum.Album.Nome
            );
        }

        public async Task Atualizar(Guid id, SalvarPaginaAlbumDTO paginaAlbumDTO)
        {
            var paginaAlbumExiste = await _paginaAlbumRepository.ObterPorIdAsync(id);

            if (paginaAlbumExiste == null)
            {
                Notificar("Não foi possível encontrar a página para alteração.");
                return;
            }

            if (paginaAlbumDTO.NumeroPagina <= 0)
            {
                Notificar("Número da página inválido.");
                return;
            }

            if (paginaAlbumDTO.Largura <= 0)
            {
                Notificar("Largura inválida.");
                return;
            }

            if (paginaAlbumDTO.Altura <= 0)
            {
                Notificar("Altura inválida.");
                return;
            }

            var paginaDuplicada = await _paginaAlbumRepository.BuscarAsync(
                x => x.AlbumId == paginaAlbumExiste.AlbumId &&
                     x.NumeroPagina == paginaAlbumDTO.NumeroPagina &&
                     x.Id != id);

            if (paginaDuplicada.Any())
            {
                Notificar("Já existe uma página com esse número cadastrada.");
                return;
            }

            paginaAlbumExiste.Atualizar(
                paginaAlbumDTO.NumeroPagina,
                paginaAlbumDTO.ImagemPagina,
                paginaAlbumDTO.Largura,
                paginaAlbumDTO.Altura);

            await _paginaAlbumRepository.AtualizarAsync(paginaAlbumExiste);
        }

        public async Task Excluir(Guid id)
        {
            var paginaAlbum = await _paginaAlbumRepository.ObterPorIdAsync(id);

            if (paginaAlbum == null)
            {
                Notificar("Página não encontrada.");
                return;
            }

            await _paginaAlbumRepository.RemoverAsync(paginaAlbum);
        }
    }
}