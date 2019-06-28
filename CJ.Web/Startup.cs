using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using CJ.Application;
using CJ.Application.Test;
using CJ.Core.Exception;
using CJ.Domain;
using CJ.Repositories;
using CJ.Repositories.Interceptor;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using AspectCore.Extensions.Autofac;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using CJ.Core;
using CJ.Data;

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
            services.AddCore(Configuration);
            services.AddScoped<ITestAppService, TestAppService>();
            services.AddTransient(typeof(IFxTest<>), typeof(FxTest<,>));
            services.AddUow();
            services.AddBaseReposity(Configuration);
            services.AddAllDbContext(Configuration);
            //Autofac容器
            var containerBuilder = new ContainerBuilder();
            //注册依赖
            services.AddUowInterceptor(containerBuilder);
            services.AddAppService(containerBuilder);
            containerBuilder.Populate(services);
            //第三方IOC接管 core内置DI容器 
            return new AutofacServiceProvider(containerBuilder.Build());
        }
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
