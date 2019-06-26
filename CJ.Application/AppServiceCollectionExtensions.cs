using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CJ.Application
{
    public static class AppServiceCollectionExtensions
    {
        public static void  AddAppService(this IServiceCollection services)
        {
            services.AddScoped<IPersonAppService, PersonAppService>();
        }
    }
}
