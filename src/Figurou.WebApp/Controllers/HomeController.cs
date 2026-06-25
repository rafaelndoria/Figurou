using Figurou.Business.Interfaces;
using Figurou.WebApp.Auth;

using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(
            INotificador notificador,
            IUsuarioAutenticado usuario) : base(notificador, usuario) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
