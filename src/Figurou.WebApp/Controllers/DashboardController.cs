using Figurou.Business.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    [Authorize]
    public class DashboardController : BaseController
    {
        public DashboardController(INotificador notificador) : base(notificador)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
