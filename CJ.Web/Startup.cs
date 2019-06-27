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
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(o => o.Filters.Add<GlobalExceptionFilter>()).SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<ITestAppService, TestAppService>();
            services.AddTransient(typeof(IFxTest<>), typeof(FxTest<,>));
            services.AddUow();
            services.AddBaseReposity(Configuration);
            services.AddAppService();

            //services.AddTransient<UnitOfWorkInterceptor>();
            services.ConfigureDynamicProxy(config => {
                //config.Interceptors.AddTyped<UnitOfWorkInterceptor>(method => method.DeclaringType.Name.EndsWith("Service"));
                config.Interceptors.AddTyped<UnitOfWorkInterceptor>(Predicates.ForService("*Repository")); //拦截所有Repository后缀的类或接口
                config.Interceptors.AddTyped<UnitOfWorkInterceptor>(Predicates.ForService("*AppService")); //拦截所有Repository后缀的类或接口
            });
           // return services.BuildAspectInjectorProvider();
            //autofac 容器
            //return services.RegisterAutofac(Configuration);
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
