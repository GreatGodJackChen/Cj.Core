using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Domain
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork: IUnitOfWork
    {
        private IConnectionStringResolver _connectionStringResolver;
        //存放处于激活状态的DbContext
        protected IDictionary<string, DbContext> ActiveDbContexts { get; }

        public UnitOfWork(IConnectionStringResolver connectionStringResolver)
        {
            _connectionStringResolver = connectionStringResolver;
        }
        public virtual TDbContext GetOrCreateDbContext<TDbContext>() where TDbContext : DbContext
        {
            var concreteDbContextType = typeof(TDbContext);
            var connectionStringResolveArgs = new Dictionary<string, object>();
            connectionStringResolveArgs["DbContextType"] = typeof(TDbContext);
            connectionStringResolveArgs["DbContextConcreteType"] = concreteDbContextType;
            var connectionString = ResolveConnectionString(connectionStringResolveArgs);

            var dbContextKey = concreteDbContextType.FullName + "#" + connectionString;
            // 创建 DbContext
            DbContext dbContext;
            //dbContext = _transactionStrategy.CreateDbContext<TDbContext>(connectionString, _dbContextResolver);
            if (!ActiveDbContexts.TryGetValue(dbContextKey, out dbContext))
            {
                //dbContext = _dbContextResolver.Resolve<TDbContext>(connectionString, null);
                ActiveDbContexts[dbContextKey] = dbContext;
            }
            return (TDbContext)dbContext; ;

        }
        protected virtual string ResolveConnectionString(Dictionary<string, object> args)
        {
            return _connectionStringResolver.GetNameOrConnectionString(args);
        }
        public int SaveChanges()
        {
            return 1;
        }
    }
}
