using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CJ.Domain
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork: IUnitOfWork
    {
        private IConnectionStringResolver _connectionStringResolver;
        private readonly IServiceProvider _serviceProvider;
        //存放处于激活状态的DbContext
        protected IDictionary<string, DbContext> ActiveDbContexts { get; }

        public UnitOfWork(IConnectionStringResolver connectionStringResolver, IServiceProvider serviceProvider)
        {
            _connectionStringResolver = connectionStringResolver;
            _serviceProvider = serviceProvider;
            ActiveDbContexts=new Dictionary<string, DbContext>();
        }
        public virtual TDbContext GetOrCreateDbContext<TDbContext>() where TDbContext : DbContext
        {

            var concreteDbContextType = typeof(TDbContext);

            var dbContextType = typeof(TDbContext).ToString();
            var dbContextKey = dbContextType;
            if (!ActiveDbContexts.TryGetValue(dbContextKey, out var dbContext))
            {
                dbContext = (TDbContext)_serviceProvider.GetService(typeof(TDbContext));



            }
            ActiveDbContexts[dbContextType] = dbContext;
            return (TDbContext)dbContext;
            //var concreteDbContextType = typeof(TDbContext);
            //var connectionStringResolveArgs = new Dictionary<string, object>();
            //connectionStringResolveArgs["DbContextType"] = typeof(TDbContext);
            //connectionStringResolveArgs["DbContextConcreteType"] = concreteDbContextType;
            //var connectionString = ResolveConnectionString(connectionStringResolveArgs);

            //var dbContextKey = concreteDbContextType.FullName + "#" + connectionString;
            //// 创建 DbContext
            //DbContext dbContext;
            ////反射出app对象
            //Assembly assembly = Assembly.GetExecutingAssembly();
            //dbContext=(TDbContext)assembly.CreateInstance(concreteDbContextType.FullName);
            ////dbContext = _transactionStrategy.CreateDbContext<TDbContext>(connectionString, _dbContextResolver);
            //if (!ActiveDbContexts.TryGetValue(dbContextKey, out dbContext))
            //{
            //    //dbContext = _dbContextResolver.Resolve<TDbContext>(connectionString, null);
            //    ActiveDbContexts[dbContextKey] = dbContext;
            //}
            //return (TDbContext)dbContext; ;
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
