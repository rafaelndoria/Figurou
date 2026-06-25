using Figurou.Business.Enums;
using Figurou.Business.Interfaces;
using Figurou.WebApp.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    [Authorize(Roles = nameof(EUsuarioRole.Admin))]
    [Route("admin/albuns/{albumId:Guid}/paginas/{paginaId:Guid}/figurinhas/{figurinhaId:Guid}/configuracoes")]
    public class SlotPaginaAlbumController : BaseController
    {
        public SlotPaginaAlbumController(
            INotificador notificador,
            IUsuarioAutenticado usuario) : base(notificador, usuario)
        {
        }

        public IActionResult Index(Guid albumId, Guid paginaId, Guid figurinhaId)
        {
            return View();
        }
    }
}
