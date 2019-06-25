using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.Uow
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class UnitOfWork: UnitOfWorkBase
    {
        private IConnectionStringResolver _connectionStringResolver;
        private readonly IServiceProvider _serviceProvider;

        private IUnitOfWorkDefaultOptions _defaultOptions;
        //存放处于激活状态的DbContext
        protected IDictionary<string, DbContext> ActiveDbContexts { get; }

        public UnitOfWork(IConnectionStringResolver connectionStringResolver, IUnitOfWorkDefaultOptions defaultOptions, IServiceProvider serviceProvider):base(connectionStringResolver, defaultOptions)
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
        public IReadOnlyList<DbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToImmutableList();
        }
        public override void SaveChanges()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                SaveChangesInDbContext(dbContext);
            }
        }
        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }
        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        protected override void CompleteUow()
        {
            SaveChanges();
            CommitTransaction();
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            CommitTransaction();
        }
        private void CommitTransaction()
        {
            if (Options.IsTransactional == true)
            {
                //_transactionStrategy.Commit();
            }
        }
        protected override void DisposeUow()
        {
            throw new NotImplementedException();
        }
    }
}
