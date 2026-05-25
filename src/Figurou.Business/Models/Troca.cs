using Figurou.Business.Enums;

namespace Figurou.Business.Models
{
    public class Troca : Entidade
    {
        protected Troca() { }

        public Troca(
            Guid solicitanteId,
            Guid destinatarioId,
            string? observacao = null)
        {
            SolicitanteId = solicitanteId;
            DestinatarioId = destinatarioId;

            Observacao = observacao;

            Status = EStatusTroca.Pendente;
            DataRequisicao = DateTime.UtcNow;

            TrocaItens = new List<TrocaItem>();
        }

        public EStatusTroca Status { get; private set; }
        public string? Observacao { get; private set; }
        public DateTime DataRequisicao { get; private set; }
        public DateTime? DataFinalizacao { get; private set; }

        public Usuario Solicitante { get; private set; } = null!;
        public Guid SolicitanteId { get; private set; }

        public Usuario Destinatario { get; private set; } = null!;
        public Guid DestinatarioId { get; private set; }

        public ICollection<TrocaItem> TrocaItens { get; private set; }

        public void AceitarTroca()
        {
            if (Status != EStatusTroca.Pendente)
                return;

            Status = EStatusTroca.Aceita;
        }

        public void FinalizarTroca()
        {
            if (Status != EStatusTroca.Aceita)
                return;

            Status = EStatusTroca.TrocaRealizada;

            DataFinalizacao = DateTime.UtcNow;
        }

        public void CancelarTroca()
        {
            if (Status == EStatusTroca.TrocaRealizada)
                return;

            Status = EStatusTroca.Cancelado;
        }

        public void AtualizarObservacao(string observacao)
        {
            Observacao = observacao;
        }
    }
}