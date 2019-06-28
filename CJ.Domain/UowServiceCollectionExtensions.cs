using CJ.Domain.EntityFrameworkCore;
using CJ.Domain.Uow;
using CJ.Domain.UowManager;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace CJ.Domain
{
    public static class UowServiceCollectionExtensions
    {
        /// <summary>
        /// 添加Unit Of Work
        /// </summary>
        /// <param name="services"></param>
        public static void AddUow(this Microsoft.Extensions.DependencyInjection.IServiceCollection services)
        {
            #region UOW

            services.TryAddSingleton<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>();

            services.AddTransient<ICurrentUnitOfWorkProvider, AsyncLocalCurrentUnitOfWorkProvider>();

            services.AddTransient<UnitOfWorkBase, EfCoreUnitOfWork>();

            services.AddTransient<IUnitOfWork, EfCoreUnitOfWork>();

            services.AddTransient<IUnitOfWorkManager, UnitOfWorkManager>();

            #endregion

            #region EntityFrameworkCore

            services.TryAddSingleton<IDbContextTypeMatcher, DbContextTypeMatcher>();

            services.AddTransient<IEfCoreTransactionStrategy, DbContextEfCoreTransactionStrategy>();

            services.AddTransient<IDbContextResolver, DefaultDbContextResolver>();

            services.AddTransient(typeof(IDbContextProvider<>), typeof(UnitOfWorkDbContextProvider<>));

            #endregion
        }
    }
    }