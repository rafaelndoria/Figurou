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
        private readonly IUsuarioRepository _usuarioRepository;

        public AlbumService(
            INotificador notificador,
            IAlbumRepository albumRepository,
            IArquivoService arquivoService,
            IUsuarioRepository usuarioRepository) : base(notificador)
        {
            _albumRepository = albumRepository;
            _arquivoService = arquivoService;
            _usuarioRepository = usuarioRepository;
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

        public async Task<AlbumVirtualDTO> BuscarAlbumVirtualUsuario(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.BuscarUsuarioCompleto(usuarioId);

            var figurinhasUsuario = usuario.FigurinhasUsuario
                .ToDictionary(x => x.FigurinhaId);

            var albumVirtualDTO = new AlbumVirtualDTO(
                usuario.Album.Id,
                usuario.Album.Nome,
                usuario.Album.TotalFigurinhas,
                usuario.FigurinhasUsuario.Count,
                usuario.FigurinhasUsuario.Where(x => x.Quantidade > 1).ToList().Count(),
                usuario.Album.TotalFigurinhas - usuario.FigurinhasUsuario.Count);

            var paginasComSelecao = usuario.Album.Paginas
                .Where(x => x.SelecaoId.HasValue)
                .OrderBy(x => x.NumeroPagina)
                .GroupBy(x => x.SelecaoId);

            foreach (var grupo in paginasComSelecao)
            {
                var grupoDTO = new GrupoAlbumVirtualDTO(
                    grupo.First().Selecao.Nome,
                    grupo.Min(x => x.NumeroPagina),
                    grupo.Max(x => x.NumeroPagina));

                foreach (var figurinha in grupo.SelectMany(x => x.Figurinhas))
                {
                    figurinhasUsuario.TryGetValue(
                        figurinha.Id,
                        out var figurinhaUsuario);

                    var quantidade = figurinhaUsuario != null ? figurinhaUsuario.Quantidade : 0;

                    grupoDTO.AdicionarFigurinha(
                        new FigurinhaAlbumVirtualDTO(
                            figurinha.Id,
                            figurinha.Codigo,
                            figurinha.Raridade,
                            quantidade,
                            figurinha.Numero,
                            figurinha.NomeJogador));
                }

                albumVirtualDTO.AdicionarGrupo(grupoDTO);
            }

            var paginasSemSelecao = usuario.Album.Paginas
                .Where(x => !x.SelecaoId.HasValue)
                .OrderBy(x => x.NumeroPagina);

            foreach (var pagina in paginasSemSelecao)
            {
                var grupoDTO = new GrupoAlbumVirtualDTO(
                    "",
                    pagina.NumeroPagina,
                    pagina.NumeroPagina);

                foreach (var figurinha in pagina.Figurinhas)
                {
                    figurinhasUsuario.TryGetValue(
                        figurinha.Id,
                        out var figurinhaUsuario);

                    var quantidade = figurinhaUsuario != null ? figurinhaUsuario.Quantidade : 0;

                    grupoDTO.AdicionarFigurinha(
                        new FigurinhaAlbumVirtualDTO(
                            figurinha.Id,
                            figurinha.Codigo,
                            figurinha.Raridade,
                            quantidade,
                            figurinha.Numero,
                            figurinha.NomeJogador));
                }

                albumVirtualDTO.AdicionarGrupo(grupoDTO);
            }

            return albumVirtualDTO;
        }
    }
}