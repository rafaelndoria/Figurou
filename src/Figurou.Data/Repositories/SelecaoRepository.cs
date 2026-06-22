using Figurou.Business.Interfaces;
using Figurou.Business.Models;
using Figurou.Data.Context;

namespace Figurou.Data.Repositories
{
    public class SelecaoRepository : Repository<Selecao>, ISelecaoRepository
    {
        public SelecaoRepository(AppDbContext db) : base(db)
        {
        }
    }
}
