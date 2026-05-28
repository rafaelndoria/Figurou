using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Figurou.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entidade, new()
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> BuscarAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorIdAsync(Guid id)
        {
            return await DbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TEntity>> ObterTodosAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task AdicionarAsync(TEntity entidade)
        {
            DbSet.Add(entidade);
            await SalvarAlteracoesAsync();
        }

        public async Task AtualizarAsync(TEntity entidade)
        {
            DbSet.Update(entidade);
            await SalvarAlteracoesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SalvarAlteracoesAsync();
        }

        public async Task<int> SalvarAlteracoesAsync()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
