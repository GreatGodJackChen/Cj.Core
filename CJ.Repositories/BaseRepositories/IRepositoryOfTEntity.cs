using Cj.Entities.BaseEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Repositories.BaseRepositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : class, IEntity<string>
    {

    }
}
