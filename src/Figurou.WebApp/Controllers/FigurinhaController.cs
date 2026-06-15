using Figurou.Business.DTOs;
using Figurou.Business.Enums;
using Figurou.Business.Interfaces;
using Figurou.Business.Services.Interfaces;
using Figurou.WebApp.InputModels;
using Figurou.WebApp.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    [Authorize(Roles = nameof(EUsuarioRole.Admin))]
    [Route("admin/albuns/{albumId:Guid}/paginas/{paginaId:Guid}/figurinhas")]
    public class FigurinhaController : BaseController
    {
        private readonly IFigurinhaService _figurinhaService;
        private readonly IPaginaAlbumService _paginaAlbumService;

        public FigurinhaController(
            INotificador notificador,
            IFigurinhaService figurinhaService,
            IPaginaAlbumService paginaAlbumService) : base(notificador)
        {
            _figurinhaService = figurinhaService;
            _paginaAlbumService = paginaAlbumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid paginaId, Guid albumId)
        {
            var figurinhasViewModel = await BuscarFigurinhasPagina(paginaId, albumId);
            return View(figurinhasViewModel);
        }

        [HttpGet("criar")]
        public IActionResult Criar(Guid paginaId, Guid albumId)
        {
            var cadastroFigurinha = new CadastroFigurinhaInputModel
            {
                PaginaId = paginaId,
                AlbumId = albumId
            };

            return View(cadastroFigurinha);
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar(CadastroFigurinhaInputModel cadastroFigurinha)
        {
            if (!ModelState.IsValid)
                return View(cadastroFigurinha);

            await _figurinhaService.Cadastrar(
                new SalvarFigurinhaDTO(
                    cadastroFigurinha.PaginaId,
                    cadastroFigurinha.AlbumId,
                    cadastroFigurinha.Codigo,
                    cadastroFigurinha.Numero,
                    cadastroFigurinha.Raridade,
                    cadastroFigurinha.NomeJogador));

            if (!OperacaoValida())
                return View(cadastroFigurinha);

            TempData["Sucesso"] = "Figurinha criada com sucesso!";

            return RedirectToAction(nameof(Criar), new
            {
                paginaId = cadastroFigurinha.PaginaId,
                albumId = cadastroFigurinha.AlbumId
            });
        }

        [HttpGet("excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id, Guid paginaId, Guid albumId)
        {
            await _figurinhaService.Deletar(id);

            if (!OperacaoValida())
            {
                return RedirectToAction(nameof(Index), new
                {
                    paginaId,
                    albumId
                });
            }

            TempData["Sucesso"] = "Figurinha excluída com sucesso!";

            return RedirectToAction(nameof(Index), new
            {
                paginaId,
                albumId
            });
        }

        [HttpGet("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(Guid id, Guid paginaId, Guid albumId)
        {
            var figurinha = await _figurinhaService.ObterPorId(id);

            if (figurinha == null)
            {
                return RedirectToAction(nameof(Index), new
                {
                    paginaId,
                    albumId
                });
            }

            var atualizarFigurinha = new AtualizarFigurinhaInputModel
            {
                Id = figurinha.Id,
                Codigo = figurinha.Codigo,
                Numero = figurinha.Numero,
                Raridade = figurinha.Raridade,
                NomeJogador = figurinha.NomeJogador,
                PaginaId = paginaId,
                AlbumId = albumId
            };

            return View(atualizarFigurinha);
        }

        [HttpPost("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(Guid id, AtualizarFigurinhaInputModel atualizarFigurinha)
        {
            if (id != atualizarFigurinha.Id)
            {
                AdicionarNotificacao("Figurinha inválida.");
                return View(atualizarFigurinha);
            }

            if (!ModelState.IsValid)
                return View(atualizarFigurinha);

            var salvarFigurinha = new SalvarFigurinhaDTO(
                atualizarFigurinha.PaginaId,
                atualizarFigurinha.AlbumId,
                atualizarFigurinha.Codigo,
                atualizarFigurinha.Numero,
                atualizarFigurinha.Raridade,
                atualizarFigurinha.NomeJogador);

            await _figurinhaService.Atualizar(id, salvarFigurinha);

            if (!OperacaoValida())
                return View(atualizarFigurinha);

            TempData["Sucesso"] = "Figurinha atualizada com sucesso.";

            return RedirectToAction(nameof(Editar), new
            {
                id,
                paginaId = atualizarFigurinha.PaginaId,
                albumId = atualizarFigurinha.AlbumId
            });
        }

        private async Task<FigurinhaViewModel> BuscarFigurinhasPagina(Guid paginaId, Guid albumId)
        {
            var paginaDTO = await _paginaAlbumService.ObterPaginaPorId(paginaId);

            var figurinhasDTO = await _figurinhaService
                .BuscarFigurinhasPorPaginaAlbum(paginaId, albumId);

            figurinhasDTO ??= Enumerable.Empty<FigurinhaDTO>();

            return new FigurinhaViewModel
            {
                PaginaId = paginaId,
                AlbumId = albumId,

                NomeAlbum = paginaDTO?.NomeAlbum ?? "Álbum",
                NumeroPagina = paginaDTO?.NumeroPagina ?? 0,

                Figurinhas = figurinhasDTO
                    .OrderBy(x => x.Numero)
                    .Select(x => new FigurinhaItemViewModel
                    {
                        Id = x.Id,
                        Codigo = x.Codigo,
                        Numero = x.Numero,
                        Raridade = x.Raridade,
                        NomeJogador = x.NomeJogador,
                        AlbumId = albumId,
                        PaginaId = paginaId
                    })
                    .ToList()
            };
        }
    }
}
