﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using CJ.Domain.EntityFrameworkCore;
using CJ.Domain.Extensions;
using CJ.Domain.UowManager;
using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.Uow
{
    public class EfCoreUnitOfWork : UnitOfWorkBase
    {
        protected IDictionary<string, DbContext> ActiveDbContexts { get; }
        protected IServiceProvider IocResolver { get; }

        private readonly IDbContextResolver _dbContextResolver;
        private readonly IDbContextTypeMatcher _dbContextTypeMatcher;
        private readonly IEfCoreTransactionStrategy _transactionStrategy;

        /// <summary>
        /// Creates a new <see cref="EfCoreUnitOfWork"/>.
        /// </summary>
        public EfCoreUnitOfWork(
            IServiceProvider iocResolver,
            IConnectionStringResolver connectionStringResolver,
            IDbContextResolver dbContextResolver,
            IUnitOfWorkDefaultOptions defaultOptions,
            IDbContextTypeMatcher dbContextTypeMatcher,
            IEfCoreTransactionStrategy transactionStrategy)
            : base(connectionStringResolver,
                  defaultOptions)
        {
            IocResolver = iocResolver;
            _dbContextResolver = dbContextResolver;
            _dbContextTypeMatcher = dbContextTypeMatcher;
            _transactionStrategy = transactionStrategy;

            ActiveDbContexts = new Dictionary<string, DbContext>();
        }

        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.InitOptions(Options);
            }
        }

        public override void SaveChanges()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                SaveChangesInDbContext(dbContext);
            }
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in GetAllActiveDbContexts())
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
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
                _transactionStrategy.Commit();
            }
        }

        public IReadOnlyList<DbContext> GetAllActiveDbContexts()
        {
            return ActiveDbContexts.Values.ToImmutableList();
        }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>(string name = null)
            where TDbContext : DbContext
        {
            var concreteDbContextType = _dbContextTypeMatcher.GetConcreteType(typeof(TDbContext));
            var connectionStringResolveArgs = new Dictionary<string, object>();
            connectionStringResolveArgs["DbContextType"] = typeof(TDbContext);
            connectionStringResolveArgs["DbContextConcreteType"] = concreteDbContextType;
            var connectionString = ResolveConnectionString(connectionStringResolveArgs);

            var dbContextKey = concreteDbContextType.FullName + "#" + connectionString;
            if (name != null)
            {
                dbContextKey += "#" + name;
            }

            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(dbContextKey, out dbContext))
            {
                if (Options.IsTransactional == true)
                {
                    dbContext = _transactionStrategy.CreateDbContext<TDbContext>(connectionString, _dbContextResolver);
                }
                else
                {
                    dbContext = _dbContextResolver.Resolve<TDbContext>();
                }

                if (Options.Timeout.HasValue &&
                    dbContext.Database.IsRelational() &&
                    !dbContext.Database.GetCommandTimeout().HasValue)
                {
                    dbContext.Database.SetCommandTimeout(Options.Timeout.Value.TotalSeconds.To<int>());
                }

                //TODO: Object materialize event
                //TODO: Apply current filters to this dbcontext

                ActiveDbContexts[dbContextKey] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        protected override void DisposeUow()
        {
            if (Options.IsTransactional == true)
            {
                _transactionStrategy.Dispose(IocResolver);
            }
            else
            {
                foreach (var context in GetAllActiveDbContexts())
                {
                    Release(context);
                }
            }

            ActiveDbContexts.Clear();
        }

        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
            await dbContext.SaveChangesAsync();
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
            //IocResolver.Release(dbContext);
        }
    }
}