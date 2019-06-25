using Autofac;
using Autofac.Extensions.DependencyInjection;
using CJ.Application.Test;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

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
        }       
    }
}
