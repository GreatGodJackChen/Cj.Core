using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using CJ.Application.Test;

namespace CJ.Application
{
    public static class AppServiceCollectionExtensions
    {
        public static void  AddAppService(this IServiceCollection services, ContainerBuilder containerBuilder)
        {
            Assembly dataAccess = Assembly.Load("CJ.Application");
            containerBuilder.RegisterAssemblyTypes(dataAccess, dataAccess)
                .Where(t => t.Name.EndsWith("AppService"))
                .AsImplementedInterfaces();

            containerBuilder.RegisterType<TestAutofacAppService>().As<ITestAutofacAppService>().InstancePerLifetimeScope();
        }
    }
}
