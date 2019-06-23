using Autofac;
using Autofac.Extensions.DependencyInjection;
using CJ.Application.Test;
using CJ.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Core.Infrastructure;
using CJ.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CJ.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider RegisterAutofac(this IServiceCollection services, IConfiguration configuration)
        {
            //实例化Autofac容器
            var containerBuilder = new ContainerBuilder();
            //注册依赖
            Register(containerBuilder, services);
            containerBuilder.Populate(services);
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(containerBuilder.Build());
        }
        private static void Register(ContainerBuilder containerBuilder, IServiceCollection services)
        {
            containerBuilder.RegisterType<TestAutofacAppService>().As<ITestAutofacAppService>().InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(EfCoreRepositoryBase<,>)).As(typeof(IRepository<>)).InstancePerDependency();
            containerBuilder.RegisterGeneric(typeof(EfCoreRepositoryBase<,,>)).As(typeof(IRepository<,>)).InstancePerDependency();
            //containerBuilder.RegisterGeneric(typeof(FxTest<,>)).As(typeof(IFxTest<>));

            var typeFinder = new TypeFinder();
            var dbRegistrars = typeFinder.FindClassesOfType<DbContext>();
            if (dbRegistrars.Any())
            {
                using (var scope=containerBuilder.Build().BeginLifetimeScope())
                {
                    foreach (var dbRegistrar in dbRegistrars)
                    {
                        //注入自定义dbcontext仓储
                        containerBuilder.RegisterType(dbRegistrar).As(IDbContext)
                        containerBuilder.RegisterGeneric(dbRegistrar).Named<>(Guid.NewGuid().ToString("N")).InstancePerLifetimeScope();
                    }
                   
                }
            }
        }
    }
}
