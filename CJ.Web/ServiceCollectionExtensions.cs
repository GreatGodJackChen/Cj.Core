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

namespace CJ.Web
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider RegisterAutofac(this IServiceCollection services, IConfiguration configuration)
        {
            //实例化Autofac容器
            var containerBuilder = new ContainerBuilder();
            //注册依赖
            Register(containerBuilder);
            containerBuilder.Populate(services);
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(containerBuilder.Build());
        }
        private static void Register(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterType<TestAutofacAppService>().As<ITestAutofacAppService>().InstancePerLifetimeScope();
            containerBuilder.RegisterGeneric(typeof(EfCoreRepository<,>)).As(typeof(IRepository<>)).InstancePerDependency();
            containerBuilder.RegisterGeneric(typeof(EfCoreRepository<,,>)).As(typeof(IRepository<,>)).InstancePerDependency();

        }
    }
}
