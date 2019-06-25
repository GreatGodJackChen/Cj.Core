using System;
using CJ.Domain.Uow;
using CJ.Domain.UowManager;
using Microsoft.EntityFrameworkCore;

namespace CJ.Domain.EntityFrameworkCore
{
    public interface IEfCoreTransactionStrategy
    {
        void InitOptions(UnitOfWorkOptions options);

        DbContext CreateDbContext<TDbContext>(string connectionString, IDbContextResolver dbContextResolver)
            where TDbContext : DbContext;

        void Commit();

        void Dispose(IServiceProvider iocResolver);
    }
}