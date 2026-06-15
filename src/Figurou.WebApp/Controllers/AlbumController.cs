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
    [Route("admin/albuns")]
    public class AlbumController : BaseController
    {
        private readonly IAlbumService _albumService;

        public AlbumController(INotificador notificador, IAlbumService albumService) : base(notificador)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var albuns = await ObterTodosAlbuns();

            return View(albuns);
        }

        [HttpGet]
        [Route("criar")]
        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [Route("criar")]
        public async Task<IActionResult> Criar(CadastroAlbumInputModel cadastroAlbum)
        {
            if (!ModelState.IsValid)
                return View(cadastroAlbum);

            if (cadastroAlbum.ImagemCapa == null)
            {
                ModelState.AddModelError(nameof(cadastroAlbum.ImagemCapa), "A imagem da capa é obrigatória.");
                return View(cadastroAlbum);
            }

            var caminhoImagem = await _albumService.AdicionarCapa(
                cadastroAlbum.ImagemCapa.OpenReadStream(),
                cadastroAlbum.ImagemCapa.FileName);

            if (!OperacaoValida() || string.IsNullOrEmpty(caminhoImagem))
                return View(cadastroAlbum);

            var albumDTO = new SalvarAlbumDTO(
                cadastroAlbum.Nome,
                cadastroAlbum.Ano,
                cadastroAlbum.Descricao,
                caminhoImagem,
                cadastroAlbum.TotalFigurinhas);

            await _albumService.Cadastrar(albumDTO);

            if (!OperacaoValida())
                return View(cadastroAlbum);

            TempData["Sucesso"] = "Álbum cadastrado com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Route("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var albumViewModel = await ObterAlbumPorId(id);

            if (!OperacaoValida() || albumViewModel == null)
                return RedirectToAction(nameof(Index));

            var atualizarAlbumInputModel = new AtualizarAlbumInputModel
            {
                Id = id,
                Nome = albumViewModel.Nome,
                Ano = albumViewModel.Ano,
                TotalFigurinhas = albumViewModel.TotalFigurinhas,
                Descricao = albumViewModel.Descricao,
                Caminho = albumViewModel.Capa
            };

            return View(atualizarAlbumInputModel);
        }

        [HttpPost]
        [Route("editar/{id:Guid}")]
        public async Task<IActionResult> Editar(AtualizarAlbumInputModel atualizarAlbum, Guid id)
        {
            if (!ModelState.IsValid)
                return View(atualizarAlbum);

            string? caminhoImagem = null;

            if (atualizarAlbum.ImagemCapa != null)
            {
                caminhoImagem = await _albumService.AtualizarCapa(
                    id,
                    atualizarAlbum.ImagemCapa.OpenReadStream(),
                    atualizarAlbum.ImagemCapa.FileName);

                if (!OperacaoValida() || string.IsNullOrEmpty(caminhoImagem))
                    return View(atualizarAlbum);
            }

            var albumDTO = new SalvarAlbumDTO(
                atualizarAlbum.Nome,
                atualizarAlbum.Ano,
                atualizarAlbum.Descricao,
                string.IsNullOrEmpty(caminhoImagem)
                    ? atualizarAlbum.Caminho
                    : caminhoImagem,
                atualizarAlbum.TotalFigurinhas);

            await _albumService.Atualizar(id, albumDTO);

            if (!OperacaoValida())
                return View(atualizarAlbum);

            TempData["Sucesso"] = "Álbum atualizado com sucesso.";

            return RedirectToAction(nameof(Editar), new { id });
        }

        [HttpPost]
        [Route("alterar-status/{id:Guid}")]
        public async Task<IActionResult> AlterarStatus(Guid id)
        {
            await _albumService.AlterarStatus(id);

            if (!OperacaoValida())
                return RedirectToAction(nameof(Index));

            TempData["Sucesso"] = "Status do álbum alterado com sucesso.";

            return RedirectToAction(nameof(Index));
        }

        private async Task<AlbumViewModel?> ObterAlbumPorId(Guid id)
        {
            var album = await _albumService.ObterPorId(id);

            if (album == null)
                return null;

            return new AlbumViewModel
            {
                Nome = album.Nome,
                Capa = album.ImagemCapa,
                Descricao = album.Descricao,
                TotalFigurinhas = album.TotalFigurinhas,
                Ano = album.Ano,
                Ativo = album.Ativo,
                Id = album.Id
            };
        }

        private async Task<IEnumerable<AlbumViewModel>> ObterTodosAlbuns()
        {
            var albuns = await _albumService.ObterTodos();

            return albuns.Select(album => new AlbumViewModel
            {
                Nome = album.Nome,
                Capa = album.ImagemCapa,
                Descricao = album.Descricao,
                TotalFigurinhas = album.TotalFigurinhas,
                Ano = album.Ano,
                Ativo = album.Ativo,
                Id = album.Id
            });
        }
    }
}