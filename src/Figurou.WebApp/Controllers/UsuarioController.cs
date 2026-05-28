using Figurou.Business.Interfaces;
using Figurou.Business.Services.Interfaces;
using Figurou.WebApp.InputModel;

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

            var usuario = await _usuarioService.Cadastrar(registroUsuario);

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

            var usuario = await _usuarioService.Login(loginUsuario);

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