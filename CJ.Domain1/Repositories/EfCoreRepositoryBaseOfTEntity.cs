using Cj.Entities.BaseEntity;
using CJ.Domain.Uow;
using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.Repositories
{
    public class EfCoreRepositoryBase<TDbContext, TEntity> : EfCoreRepositoryBase<TDbContext, TEntity, string>, IRepository<TEntity>
        where TEntity : class, IEntity<string>
        where TDbContext : DbContext
    {
        public EfCoreRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}
