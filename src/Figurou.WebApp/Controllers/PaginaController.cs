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
    [Route("admin/album/{albumId:Guid}/paginas")]
    public class PaginaController : BaseController
    {
        private readonly IPaginaAlbumService _paginaAlbumService;
        private readonly IArquivoService _arquivoService;

        public PaginaController(
            INotificador notificador,
            IPaginaAlbumService paginaAlbumService,
            IArquivoService arquivoService) : base(notificador)
        {
            _paginaAlbumService = paginaAlbumService;
            _arquivoService = arquivoService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid albumId)
        {
            var paginasViewModel = await BuscarPaginasAlbum(albumId);
            return View(paginasViewModel);
        }

        [HttpGet("criar")]
        public IActionResult Criar(Guid albumId)
        {
            return View(new CriarPaginaAlbumInputModel { AlbumId = albumId });
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar(CriarPaginaAlbumInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return View(inputModel);

            if (inputModel.Imagem == null)
            {
                ModelState.AddModelError(nameof(inputModel.Imagem), "A imagem da página é obrigatória.");
                return View(inputModel);
            }

            if (await _paginaAlbumService.ExistePagina(inputModel.AlbumId, inputModel.NumeroPagina))
            {
                ModelState.AddModelError(nameof(inputModel.NumeroPagina), "Já existe uma página com esse número cadastrada.");
                return View(inputModel);
            }

            var caminhoImagem = await _arquivoService.SalvarAsync(
                inputModel.Imagem.OpenReadStream(),
                inputModel.Imagem.FileName,
                "uploads/paginas-album");

            if (!OperacaoValida() || string.IsNullOrEmpty(caminhoImagem))
                return View(inputModel);

            var paginaAlbumDTO = new SalvarPaginaAlbumDTO(
                Guid.NewGuid(),
                inputModel.NumeroPagina,
                caminhoImagem,
                inputModel.Largura,
                inputModel.Altura,
                inputModel.AlbumId);

            await _paginaAlbumService.Adicionar(paginaAlbumDTO);

            if (!OperacaoValida())
                return View(inputModel);

            ViewBag.Sucesso = "Página criada com sucesso!";
            return RedirectToAction(nameof(Index), new { albumId = inputModel.AlbumId });
        }

        [HttpGet("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(Guid id, Guid albumId)
        {
            var paginaAlbumDTO = await _paginaAlbumService.ObterPaginaPorId(id);
            if (!OperacaoValida() || paginaAlbumDTO == null)
                return RedirectToAction(nameof(Index), new { albumId });

            var inputModel = new AtualizarPaginaAlbumInputModel
            {
                Id = paginaAlbumDTO.Id,
                AlbumId = albumId,
                NumeroPagina = paginaAlbumDTO.NumeroPagina,
                ImagemPagina = paginaAlbumDTO.ImagemPagina,
                Largura = paginaAlbumDTO.Largura,
                Altura = paginaAlbumDTO.Altura
            };

            return View(inputModel);
        }

        [HttpPost("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(Guid id, Guid albumId, AtualizarPaginaAlbumInputModel inputModel)
        {
            if (!ModelState.IsValid)
                return View(inputModel);

            if (inputModel.NovaImagem != null)
            {
                if (!_arquivoService.DeletarImagem(inputModel.ImagemPagina))
                {
                    AdicionarNotificacao("Não foi possível deletar a imagem anterior.");
                    return View(inputModel);
                }

                var novoCaminho = await _arquivoService.SalvarAsync(
                    inputModel.NovaImagem.OpenReadStream(),
                    inputModel.NovaImagem.FileName,
                    "uploads/paginas-album");

                if (string.IsNullOrEmpty(novoCaminho))
                {
                    AdicionarNotificacao("Não foi possível salvar a nova imagem.");
                    return View(inputModel);
                }

                inputModel.ImagemPagina = novoCaminho;
            }

            var atualizarDTO = new SalvarPaginaAlbumDTO(
                inputModel.Id,
                inputModel.NumeroPagina,
                inputModel.ImagemPagina,
                inputModel.Largura,
                inputModel.Altura,
                inputModel.AlbumId);

            await _paginaAlbumService.Atualizar(id, atualizarDTO);

            if (!OperacaoValida())
                return View(inputModel);

            ViewBag.Sucesso = "Página atualizada com sucesso!";
            return RedirectToAction(nameof(Editar), new { id, albumId });
        }

        [HttpGet("excluir/{id:Guid}")]
        public async Task<IActionResult> Excluir(Guid id, Guid albumId)
        {
            await _paginaAlbumService.Excluir(id);
            return RedirectToAction(nameof(Index), new { albumId });
        }

        private async Task<PaginaAlbumViewModel> BuscarPaginasAlbum(Guid albumId)
        {
            var paginasDTO = await _paginaAlbumService.BuscarPaginasPorAlbum(albumId);
            paginasDTO ??= Enumerable.Empty<PaginaAlbumDTO>();

            var primeiraPagina = paginasDTO.FirstOrDefault();

            return new PaginaAlbumViewModel
            {
                AlbumId = albumId,
                NomeAlbum = primeiraPagina?.NomeAlbum ?? "Álbum",
                Paginas = paginasDTO.Select(x => new PaginaAlbumItemViewModel
                {
                    Id = x.Id,
                    NumeroPagina = x.NumeroPagina,
                    ImagemPagina = x.ImagemPagina,
                    AlbumId = x.AlbumId
                }).OrderBy(x => x.NumeroPagina).ToList()
            };
        }
    }
}