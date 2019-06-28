using System;
using CJ.Data.FirstModels;
using CJ.Data.Logger;
using CJ.Data.SecondTestModel;
using CJ.Domain.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CJ.Data
{
    public static class DbContextServiceCollectionExtensions
    {
        /// <summary>
        /// 依赖注入所有的DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAllDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            LoggerFactory LoggerFactory = new LoggerFactory(new[] { new MyFilteredLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information) });
            var uowconn = configuration["ConnectionStrings:Default"];
            var uowOptions = new DbContextOptionsBuilder<FirstTestDBContext>()
                .UseSqlServer(uowconn)
                .UseLoggerFactory(LoggerFactory)
                .Options;
            services.AddSingleton(uowOptions).AddTransient(typeof(FirstTestDBContext));

            var secondconn = configuration["ConnectionStrings:SecondTestDbB"];
            var secondOptions = new DbContextOptionsBuilder<SecondTestDBContext>()
                .UseSqlServer(secondconn)
                .UseLoggerFactory(LoggerFactory)
                .Options;
            services.AddSingleton(secondOptions).AddTransient(typeof(SecondTestDBContext));

            services.AddTransient<IConnectionStringResolver, MyConnectionStringResolver>();
        }
    }
}