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
    [Route("usuarios")]
    [Authorize]
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;
        private readonly IAlbumRepository _albumRepository;

        public UsuarioController(
            INotificador notificador,
            IUsuarioAutenticado usuario,
            IUsuarioService usuarioService,
            IAuthService authService,
            IAlbumRepository albumRepository) : base(notificador, usuario)
        {
            _usuarioService = usuarioService;
            _authService = authService;
            _albumRepository = albumRepository;
        }

        [AllowAnonymous]
        [HttpGet("novo-usuario")]
        public IActionResult Registrar()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("novo-usuario")]
        public async Task<IActionResult> Registrar(RegistroUsuarioInputModel registroUsuario)
        {
            if (!ModelState.IsValid)
                return View(registroUsuario);

            var cadastrarUsuarioDTO = new CadastrarUsuarioDTO(registroUsuario.Username, registroUsuario.Email, registroUsuario.Senha);
            var usuario = await _usuarioService.Cadastrar(cadastrarUsuarioDTO);

            if (!OperacaoValida())
                return View(registroUsuario);

            var token = _authService.GerarJwtToken(
                usuario.Email,
                usuario.Username,
                usuario.Papel,
                usuario.Id,
                usuario?.AlbumEscolhidoId);

            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            if (usuario?.AlbumEscolhidoId != null)
                _usuario.DefinirAlbum((Guid)usuario.AlbumEscolhidoId);

            TempData["Sucesso"] = "Usuário cadastrado com sucesso.";

            return RedirectToAction(nameof(Login));
        }

        [AllowAnonymous]
        [HttpGet("entrar")]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost("entrar")]
        public async Task<IActionResult> Login(LoginUsuarioInputModel loginUsuario)
        {
            if (!ModelState.IsValid)
                return View(loginUsuario);

            var loginUsuarioDTO = new LoginUsuarioDTO(loginUsuario.Username, loginUsuario.Senha);
            var usuario = await _usuarioService.Login(loginUsuarioDTO);

            if (!OperacaoValida())
                return View(loginUsuario);

            var token = _authService.GerarJwtToken(
                usuario.Email,
                usuario.Username,
                usuario.Papel,
                usuario.Id,
                usuario?.AlbumEscolhidoId);

            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            if (usuario?.AlbumEscolhidoId != null)
                _usuario.DefinirAlbum((Guid)usuario.AlbumEscolhidoId);

            TempData["Sucesso"] = "Login realizado com sucesso.";

            return RedirectToAction("Index", "Dashboard");
        }

        [HttpGet("escolher-album")]
        public async Task<IActionResult> EscolherAlbum()
        {
            var albuns = await _albumRepository.BuscarAsync(x => x.Ativo == true);
            var albumId = AlbumId;

            var albumViewModel = albuns.Select(x => new AlbumViewModel
            {
                Nome = x.Nome,
                Capa = x.ImagemCapa,
                Descricao = x.Descricao,
                TotalFigurinhas = x.TotalFigurinhas,
                Ano = x.Ano,
                Id = x.Id,
                UsuarioSelecionado = albumId != null || albumId == x.Id ? true : false
            });

            return View(albumViewModel);
        }

        [HttpPost("escolher-album")]
        public async Task<IActionResult> EscolherAlbum(Guid albumId)
        {
            await _usuarioService.EscolherAlbumUsuario(UsuarioId, albumId);

            if (!OperacaoValida())
                return RedirectToAction(nameof(EscolherAlbum));

            _usuario.DefinirAlbum(albumId);
            return RedirectToAction(nameof(EscolherAlbum));
        }

        [HttpPost("remover-album")]
        public async Task<IActionResult> RemoverAlbum(Guid albumId)
        {
            await _usuarioService.RemoverAlbumUsuario(UsuarioId, albumId);

            if (!OperacaoValida())
                return RedirectToAction(nameof(EscolherAlbum));

            _usuario.LimparAlbum();
            return RedirectToAction(nameof(EscolherAlbum));
        }
    }
}