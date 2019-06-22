using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain.Repositories
{
    public interface IRepository<TEntity> : IRepository<TEntity, string> where TEntity : class, IEntity<string>
    {

    }
}
