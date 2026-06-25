using Figurou.Business.DTOs;
using Figurou.Business.Interfaces;
using Figurou.Business.Services.Interfaces;
using Figurou.WebApp.Auth;
using Figurou.WebApp.InputModels;
using Figurou.WebApp.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    [Authorize]
    [Route("album-virtual")]
    public class AlbumVirtualController : BaseController
    {
        private readonly IAlbumService _albumService;
        private readonly IFigurinhaService _figurinhaService;

        public AlbumVirtualController(
            INotificador notificador,
            IUsuarioAutenticado usuario,
            IAlbumService albumService,
            IFigurinhaService figurinhaService) : base(notificador, usuario)
        {
            _albumService = albumService;
            _figurinhaService = figurinhaService;
        }

        public async Task<IActionResult> Index()
        {
            var albumVirtualDTO = await _albumService.BuscarAlbumVirtualUsuario(UsuarioId);

            var albumVirtualViewModel = new AlbumVirtualViewModel
            {
                AlbumId = albumVirtualDTO.AlbumId,
                NomeAlbum = albumVirtualDTO.NomeAlbum,
                TotalFigurinhas = albumVirtualDTO.TotalFigurinhas,
                TotalPossuidas = albumVirtualDTO.TotalPossuidas,
                TotalRepetidas = albumVirtualDTO.TotalRepetidas,
                TotalFaltantes = albumVirtualDTO.TotalFaltantes,
                Grupos = albumVirtualDTO.Grupos.OrderBy(x => x.PaginaFinal).Select(x => new GrupoAlbumVirtualViewModel
                {
                    NomeGrupo = x.NomeGrupo,
                    PaginaInicial = x.PaginaInicial,
                    PaginaFinal = x.PaginaFinal,
                    Figurinhas = x.Figurinhas.OrderBy(x => x.Numero).Select(x => new FigurinhaAlbumVirtualViewModel
                    {
                        FigurinhaId = x.FigurinhaId,
                        Codigo = x.Codigo,
                        Raridade = x.Raridade,
                        Quantidade = x.Quantidade,
                        NomeJogador = x.NomeJogador
                    })
                })
            };

            return View(albumVirtualViewModel);
        }

        [HttpPost("salvar")]
        public async Task<IActionResult> SalvarAlteracoes([FromBody] SalvarAlteracoesAlbumVirtualViewModel model)
        {
            await _figurinhaService.AdicionarFigurinhaUsuario(model.AlbumId, UsuarioId, model.Figurinhas.Select(x => new SalvarFigurinhaUsuarioDTO(x.FigurinhaId, x.Quantidade)));

            return RedirectToAction(nameof(Index));
        }
    }
}
