using Figurou.Business.Interfaces;
using Figurou.Business.Notificacoes;
using Figurou.WebApp.Auth;

using Microsoft.AspNetCore.Mvc;

namespace Figurou.WebApp.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificador _notificador;
        protected readonly IUsuarioAutenticado _usuario;

        protected BaseController(INotificador notificador, IUsuarioAutenticado usuario)
        {
            _notificador = notificador;
            _usuario = usuario;
        }

        protected Guid UsuarioId => _usuario.UsuarioId;
        protected Guid? AlbumId => _usuario.AlbumId;

        protected void AdicionarNotificacao(string mensagem)
        {
            _notificador.AdicionarNotificacao(new Notificacao(mensagem));
        }

        protected bool OperacaoValida()
        {
            if (!_notificador.TemNotificacao()) return true;

            var notificacoes = _notificador.ObterNotificacoes();

            notificacoes.ForEach(n => ViewData.ModelState.AddModelError(string.Empty, n.Mensagem));

            return false;
        }

    }
}
