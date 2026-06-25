using Figurou.Business.Interfaces;
using Figurou.WebApp.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public DashboardController(INotificador notificador, IUsuarioAutenticado usuario) : base(notificador, usuario)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
