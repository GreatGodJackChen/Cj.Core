using CJ.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Data.FirstModels
{
    public static class UnitOfWorkServiceCollectionExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            //services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

            return services;
        }
    }
}
