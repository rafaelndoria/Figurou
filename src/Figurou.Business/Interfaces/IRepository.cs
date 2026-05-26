using Figurou.Business.Models;

using System.Linq.Expressions;

namespace Figurou.Business.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entidade
    {
        Task AdicionarAsync(TEntity entidade);
        Task<TEntity> ObterPorIdAsync(Guid id);
        Task<List<TEntity>> ObterTodosAsync();
        Task AtualizarAsync(TEntity entidade);
        Task RemoverAsync(Guid id);
        Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate);
        Task<int> SalvarAlteracoesAsync();
    }
}
