using Figurou.Business.DTOs;
using Figurou.Business.Interfaces;
using Figurou.Business.Models;

namespace Figurou.Business.Services.Interfaces
{
    public class SelecaoService : BaseService, ISelecaoService
    {
        private readonly ISelecaoRepository _selecaoRepository;
        private readonly IAlbumRepository _albumRepository;

        public SelecaoService(INotificador notificador, ISelecaoRepository selecaoRepository, IAlbumRepository albumRepository) : base(notificador)
        {
            _selecaoRepository = selecaoRepository;
            _albumRepository = albumRepository;
        }

        public async Task<IEnumerable<SelecaoDTO>> BuscarSelecoesAlbum(Guid albumId)
        {
            if (albumId == Guid.Empty)
            {
                Notificar("Álbum inválido.");
                return Enumerable.Empty<SelecaoDTO>();
            }

            var selecoes = await _selecaoRepository.BuscarAsync(
                x => x.AlbumId == albumId);

            var album = await _albumRepository.ObterPorIdAsync(albumId);

            if (selecoes == null || !selecoes.Any())
                return Enumerable.Empty<SelecaoDTO>();

            return selecoes.Select(x =>
                new SelecaoDTO(
                    x.Codigo,
                    x.Nome,
                    album.Nome + " - " + album.Descricao,
                    x.Id,
                    x.AlbumId));
        }

        public async Task Criar(SalvarSelecaoDTO salvarSelecaoDTO)
        {
            var existeSelecaoCodigoNome = await _selecaoRepository.BuscarAsync(x => x.AlbumId == salvarSelecaoDTO.AlbumId && (x.Codigo == salvarSelecaoDTO.Codigo || x.Nome == salvarSelecaoDTO.Nome));
            if (existeSelecaoCodigoNome.Any())
            {
                Notificar("Já existe uma seleção cadastrada nesse álbum com mesmo nome ou código.");
                return;
            }

            var selecao = new Selecao(salvarSelecaoDTO.Codigo, salvarSelecaoDTO.Nome, salvarSelecaoDTO.AlbumId);

            await _selecaoRepository.AdicionarAsync(selecao);
        }

        public async Task Deletar(Guid id)
        {
            var selecao = await _selecaoRepository.ObterPorIdAsync(id);

            if (selecao == null)
            {
                Notificar("Não foi encontrado a seleção para exclusão.");
                return;
            }

            await _selecaoRepository.RemoverAsync(selecao);
        }

        public async Task Atualizar(Guid id, SalvarSelecaoDTO salvarSelecaoDTO)
        {
            var selecao = await _selecaoRepository.ObterPorIdAsync(id);

            if (selecao == null)
            {
                Notificar("Seleção não encontrada para atualização.");
                return;
            }

            selecao.Atualizar(salvarSelecaoDTO.Codigo, salvarSelecaoDTO.Nome);

            await _selecaoRepository.AtualizarAsync(selecao);
        }
    }
}
