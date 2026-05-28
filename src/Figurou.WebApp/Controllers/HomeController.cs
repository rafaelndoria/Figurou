using Figurou.Business.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(INotificador notificador) : base(notificador) { }

        public IActionResult Index()
        {
            return View();
        }
    }
}
