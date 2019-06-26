using CJ.Application;
using CJ.Application.Test;
using CJ.Core.Exception;
using CJ.Domain;
using CJ.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            ////////////////////
            ////注入工作单元
            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            ////注册仓储DBcontext
            //services.AddTransient(typeof(IDbContextProvider<>), typeof(DbContextProvider<>));
            //services.AddTransient<IConnectionStringResolver, MyConnectionStringResolver>();
            ////找到所有的DBcontext
            //var typeFinder = new TypeFinder();
            //var dbContextTypes = typeFinder.FindClassesOfType<DbContext>();
            //var contextTypes = dbContextTypes as Type[] ?? dbContextTypes.ToArray();
            //if (!contextTypes.Any())
            //{
            //    throw new Exception("没有找到任何数据库访问上下文");
            //}
            //foreach (var dbContextType in contextTypes)
            //{
            //    //注入dbcontext

            //    var uowconn = Configuration["ConnectionStrings:Default"];
            //    var uowOptions = new DbContextOptionsBuilder<FirstTestDBContext>()
            //        .UseSqlServer(uowconn)
            //        .Options;
            //    services.AddSingleton(uowOptions).AddTransient(typeof(FirstTestDBContext));
            //    //注入每个实体仓库
            //    var entities = from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            //                   where
            //                       (ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) ||
            //                        ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbQuery<>))) &&
            //                       ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0],
            //                           typeof(IEntity<>))
            //                   select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
            //    foreach (var entity in entities)
            //    {
            //        var primaryKeyType = ReflectionHelper.GetPrimaryKeyType(entity.EntityType);
            //        var protype = typeof(IRepository<>).MakeGenericType(entity.EntityType);
            //        var eFprotype = typeof(EfCoreRepositoryBase<,>).MakeGenericType(entity.DeclaringType, entity.EntityType);
            //        var protypekey = typeof(IRepository<,>).MakeGenericType(entity.EntityType, primaryKeyType);
            //        var eFprotypekey = typeof(EfCoreRepositoryBase<,,>).MakeGenericType(entity.DeclaringType, entity.EntityType, primaryKeyType);
            //        services.AddTransient(protype, eFprotype);
            //        services.AddTransient(protypekey, eFprotypekey);
            //    }
            //}
            //services.AddScoped<IPersonAppService, PersonAppService>();
            //services.AddSingleton<IUnitOfWorkDefaultOptions, UnitOfWorkDefaultOptions>();
            //services.AddScoped<ICurrentUnitOfWorkProvider, AsyncLocalCurrentUnitOfWorkProvider>();
            //services.AddScoped<IUnitOfWorkCompleteHandle, InnerUnitOfWorkCompleteHandle>();
            services.BuildServiceProvider();
            ///////////////////////////////////////
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
