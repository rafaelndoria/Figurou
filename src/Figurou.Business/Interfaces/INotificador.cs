using Figurou.Business.Notificacoes;

namespace Figurou.Business.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void AdicionarNotificacao(Notificacao notificacao);
    }
}
