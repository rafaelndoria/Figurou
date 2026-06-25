using Figurou.Business.DTOs;
using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Business.Services.Interfaces;

namespace Figurou.Business.Services.Implementations
{
    public class FigurinhaService : BaseService, IFigurinhaService
    {
        private readonly IFigurinhaRepository _figurinhaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IFigurinhaUsuarioRepository _figurinhaUsuarioRepository;

        public FigurinhaService(
            INotificador notificador,
            IFigurinhaRepository figurinhaRepository,
            IUsuarioRepository usuarioRepository,
            IFigurinhaUsuarioRepository figurinhaUsuarioRepository) : base(notificador)
        {
            _figurinhaRepository = figurinhaRepository;
            _usuarioRepository = usuarioRepository;
            _figurinhaUsuarioRepository = figurinhaUsuarioRepository;
        }

        public async Task<FigurinhaDTO?> ObterPorId(Guid id)
        {
            var figurinha = await _figurinhaRepository.ObterPorIdAsync(id);

            if (figurinha == null)
                return null;

            return new FigurinhaDTO(
                figurinha.Id,
                figurinha.PaginaAlbumId,
                figurinha.AlbumId,
                figurinha.Album?.Descricao ?? string.Empty,
                figurinha.PaginaAlbum?.NumeroPagina ?? 0,
                figurinha.Codigo,
                figurinha.Numero,
                figurinha.Raridade,
                figurinha.NomeJogador
            );
        }

        public async Task Atualizar(Guid id, SalvarFigurinhaDTO figurinhaDTO)
        {
            var figurinha = await _figurinhaRepository.ObterPorIdAsync(id);

            if (figurinha == null)
            {
                Notificar("Figurinha não encontrada");
                return;
            }

            var paginaFigurinhas = await _figurinhaRepository
                .BuscarFigurasPorPagina(figurinhaDTO.PaginaId, figurinhaDTO.AlbumId);

            if (paginaFigurinhas?.Figurinhas == null)
            {
                Notificar("Figurinha não pertence ao álbum ou página informada.");
                return;
            }

            var figurinhaPertencePaginaAlbum =
                paginaFigurinhas.Figurinhas.Any(x => x.Id == id);

            if (!figurinhaPertencePaginaAlbum)
            {
                Notificar("Figurinha não pertence ao álbum ou página informada.");
                return;
            }

            var existeCodigo = paginaFigurinhas.Figurinhas
                .Where(x => x.Id != id)
                .Any(x => x.Codigo == figurinhaDTO.Codigo);

            if (existeCodigo)
            {
                Notificar("Já existe uma figurinha com esse código.");
                return;
            }

            var existeNumero = paginaFigurinhas.Figurinhas
                .Where(x => x.Id != id)
                .Any(x => x.Numero == figurinhaDTO.Numero);

            if (existeNumero)
            {
                Notificar("Já existe uma figurinha com esse número.");
                return;
            }

            figurinha.Atualizar(
                figurinhaDTO.Codigo.ToUpper(),
                figurinhaDTO.Numero,
                figurinhaDTO.TipoRaridade,
                figurinhaDTO.NomeJogador.ToUpper());

            await _figurinhaRepository.AtualizarAsync(figurinha);
        }

        public async Task<IEnumerable<FigurinhaDTO>> BuscarFigurinhasPorPaginaAlbum(Guid paginaId, Guid albumId)
        {
            var paginaFigurinhas = await _figurinhaRepository
                .BuscarFigurasPorPagina(paginaId, albumId);

            if (paginaFigurinhas == null)
            {
                Notificar("Página do álbum não encontrada.");
                return Enumerable.Empty<FigurinhaDTO>();
            }

            if (paginaFigurinhas.Figurinhas == null || !paginaFigurinhas.Figurinhas.Any())
                return Enumerable.Empty<FigurinhaDTO>();

            return paginaFigurinhas.Figurinhas
                .OrderBy(x => x.Numero)
                .Select(x => new FigurinhaDTO(
                    x.Id,
                    x.PaginaAlbumId,
                    x.AlbumId,
                    x.Album.Descricao,
                    x.PaginaAlbum.NumeroPagina,
                    x.Codigo,
                    x.Numero,
                    x.Raridade,
                    x.NomeJogador
                ));
        }

        public async Task Cadastrar(SalvarFigurinhaDTO figurinhaDTO)
        {
            var paginaAlbum = await _figurinhaRepository
                .BuscarFigurasPorPagina(figurinhaDTO.PaginaId, figurinhaDTO.AlbumId);

            if (paginaAlbum == null)
            {
                Notificar("Página do álbum não encontrada.");
                return;
            }

            var codigo = figurinhaDTO.Codigo.Trim().ToUpper();
            var nomeJogador = figurinhaDTO.NomeJogador?.Trim().ToUpper();

            var existeCodigoFigurinha = paginaAlbum.Figurinhas
                .Any(x => x.Codigo.ToUpper() == codigo);

            if (existeCodigoFigurinha)
            {
                Notificar("Já existe uma figurinha com esse código.");
                return;
            }

            var existeNumeroFigurinha = paginaAlbum.Figurinhas
                .Any(x => x.Numero == figurinhaDTO.Numero);

            if (existeNumeroFigurinha)
            {
                Notificar("Já existe uma figurinha com esse número.");
                return;
            }

            var figurinha = new Figurinha(
                codigo,
                figurinhaDTO.Numero,
                figurinhaDTO.TipoRaridade,
                figurinhaDTO.AlbumId,
                figurinhaDTO.PaginaId,
                nomeJogador);

            await _figurinhaRepository.AdicionarAsync(figurinha);
        }

        public async Task Deletar(Guid id)
        {
            var figurinha = await _figurinhaRepository.ObterPorIdAsync(id);

            if (figurinha == null)
            {
                Notificar("Figurinha não encontrada para exclusão.");
                return;
            }

            await _figurinhaRepository.RemoverAsync(figurinha);
        }

        public async Task AdicionarFigurinhaUsuario(Guid albumId, Guid usuarioId, IEnumerable<SalvarFigurinhaUsuarioDTO> figurinhas)
        {
            var usuario = await _usuarioRepository
                .BuscarUsuarioFigurinhasColetadas(usuarioId);

            foreach (var figurinha in figurinhas)
            {
                var figurinhaExistente = usuario.FigurinhasUsuario
                    .FirstOrDefault(x => x.FigurinhaId == figurinha.FigurinhaId);

                if (figurinhaExistente == null)
                {
                    var figurinhaUsuario = new FigurinhaUsuario(figurinha.FigurinhaId, usuarioId, figurinha.Quantidade);
                    _figurinhaUsuarioRepository.AdicionarSalvar(figurinhaUsuario);
                }
                else
                {
                    figurinhaExistente.Atualizar(figurinha.Quantidade);
                }
            }

            await _figurinhaUsuarioRepository.SalvarAlteracoesAsync();
        }
    }
}