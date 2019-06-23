using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CJ.Application.Test;
using CJ.Core.Exception;
using CJ.Data.FirstModels;
using CJ.Domain;
using CJ.Domain.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CJ.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
                services.AddMvc(o => o.Filters.Add<GlobalExceptionFilter>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
                services.AddDbContext<FirstTestDBContext>();
                //test 自带di容器
                services.AddScoped<ITestAppService, TestAppService>();
                ////注入泛型仓储
                //services.AddScoped<IConnectionStringResolver, MyConnectionStringResolver>();
                 services.AddTransient(typeof(IFxTest<>),typeof(FxTest<,>));
                //services.AddTransient(typeof(IRepository<>), typeof(EfCoreRepositoryBase<,>));
                //services.AddTransient(typeof(IRepository<,>), typeof(EfCoreRepositoryBase<,,>));
                //autofac 容器
                return services.RegisterAutofac(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
