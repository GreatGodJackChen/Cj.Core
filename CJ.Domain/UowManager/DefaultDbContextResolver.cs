using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Domain.UowManager
{
    public class DefaultDbContextResolver: IDbContextResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public DefaultDbContextResolver(IServiceProvider iocResolver)
        {
            _serviceProvider = iocResolver;
        }

        public TDbContext Resolve<TDbContext>() where TDbContext : DbContext
        {
            var options = _serviceProvider.GetService<DbContextOptions<TDbContext>>();
            //默认AddDbcontext使用的是 Scope的作用域，UOW使用的是 Transient
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), new object[] { options });
        }
    }
}