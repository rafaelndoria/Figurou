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
    [Route("admin/albuns/{albumId:Guid}/selecoes")]
    public class SelecaoController : BaseController
    {
        private readonly ISelecaoService _selecaoService;
        private readonly ISelecaoRepository _selecaoRepository;

        public SelecaoController(INotificador notificador, ISelecaoService selecaoService, ISelecaoRepository selecaoRepository) : base(notificador)
        {
            _selecaoService = selecaoService;
            _selecaoRepository = selecaoRepository;
        }

        public async Task<IActionResult> Index(Guid albumId)
        {
            var selecaoViewModel = await BuscarSelecoesPorAlbum(albumId);

            return View(selecaoViewModel);
        }

        [HttpGet("criar")]
        public IActionResult Criar(Guid albumId)
        {
            var cadastroSelecao = new CadastroSelecaoInputModel
            {
                AlbumId = albumId
            };

            return View(cadastroSelecao);
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar(CadastroSelecaoInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return View(inputModel);

            await _selecaoService.Criar(new SalvarSelecaoDTO(inputModel.Codigo.ToUpper(), inputModel.Nome, inputModel.AlbumId));

            if (!OperacaoValida())
                return View(inputModel);

            TempData["Sucesso"] = "Seleção cadastrada com sucesso";
            return RedirectToAction(nameof(Criar), new
            {
                albumId = inputModel.AlbumId
            });
        }

        [HttpGet("excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id, Guid albumId)
        {
            await _selecaoService.Deletar(id);

            return RedirectToAction(nameof(Index), new
            {
                albumId = albumId
            });
        }

        [HttpGet("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(Guid id, Guid albumId)
        {
            var selecao = await _selecaoRepository.ObterPorIdAsync(id);

            if (selecao == null)
            {
                TempData["Erro"] = "Seleção não encontrada para edição.";
                return RedirectToAction(nameof(Index), new
                {
                    albumId = albumId
                });
            }

            var selecaoAtualizar = new CadastroSelecaoInputModel
            {
                Id = selecao.Id,
                AlbumId = selecao.AlbumId,
                Codigo = selecao.Codigo,
                Nome = selecao.Nome
            };

            return View(selecaoAtualizar);
        }

        [HttpPost("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(Guid id, Guid albumId, CadastroSelecaoInputModel atualizarSelecao)
        {
            if (!ModelState.IsValid)
                return View(atualizarSelecao);

            await _selecaoService.Atualizar(id, new SalvarSelecaoDTO(
                atualizarSelecao.Codigo.ToUpper(),
                atualizarSelecao.Nome,
                atualizarSelecao.AlbumId));

            if (!OperacaoValida())
                return View(atualizarSelecao);

            TempData["Sucesso"] = "Seleção atualizada com sucesso";
            return View(atualizarSelecao);
        }

        private async Task<SelecaoListaViewModel> BuscarSelecoesPorAlbum(Guid albumId)
        {
            var selecoesDTO = await _selecaoService.BuscarSelecoesAlbum(albumId);

            selecoesDTO ??= Enumerable.Empty<SelecaoDTO>();

            return new SelecaoListaViewModel
            {
                AlbumId = albumId,

                NomeAlbum = selecoesDTO
                    .FirstOrDefault()?.NomeAlbum ?? "Álbum",

                Selecoes = selecoesDTO
                    .OrderBy(x => x.Codigo)
                    .Select(x => new SelecaoViewModel
                    {
                        Id = x.Id,
                        AlbumId = x.AlbumId,
                        Codigo = x.Codigo,
                        Nome = x.Nome
                    })
                    .ToList()
            };
        }
    }
}
