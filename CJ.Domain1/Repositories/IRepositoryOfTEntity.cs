using Cj.Entities.BaseEntity;

namespace CJ.Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : class, IEntity<string>
    {

    }
}
