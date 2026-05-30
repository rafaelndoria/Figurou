using Figurou.Business.DTOs;
using Figurou.Business.Interfaces;
using Figurou.Business.Services.Interfaces;
using Figurou.WebApp.InputModels;

using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    public class UsuarioController : BaseController
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IAuthService _authService;

        public UsuarioController(
            INotificador notificador,
            IUsuarioService usuarioService,
            IAuthService authService) : base(notificador)
        {
            _usuarioService = usuarioService;
            _authService = authService;
        }

        [HttpGet]
        [Route("novo-usuario")]
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [Route("novo-usuario")]
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
                usuario.Id);

            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            TempData["Sucesso"] = "Usuário cadastrado com sucesso.";

            return RedirectToAction(nameof(Login));
        }

        [HttpGet]
        [Route("entrar")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("entrar")]
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
                usuario.Id);

            Response.Cookies.Append("access_token", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            });

            TempData["Sucesso"] = "Login realizado com sucesso.";

            return RedirectToAction("Index", "Dashboard");
        }
    }
}