using Cj.Entities.BaseEntity;
using CJ.Core.Infrastructure;
using CJ.Core.Reflection;
using CJ.Data.FirstModels;
using CJ.Repositories.BaseRepositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using CJ.Repositories.Interceptor;

namespace CJ.Repositories
{
    public static  class ReposityServiceCollectionExtensions
    {
        public static  void AddBaseReposity(this IServiceCollection services, IConfiguration Configuration)
        {
            //找到所有的DBcontext
            var typeFinder = new TypeFinder();
            var dbContextTypes = typeFinder.FindClassesOfType<DbContext>();
            var contextTypes = dbContextTypes as Type[] ?? dbContextTypes.ToArray();
            if (!contextTypes.Any())
            {
                throw new Exception("没有找到任何数据库访问上下文");
            }
            foreach (var dbContextType in contextTypes)
            {
                //注入dbcontext

                var uowconn = Configuration["ConnectionStrings:Default"];
                var uowOptions = new DbContextOptionsBuilder<FirstTestDBContext>()
                    .UseSqlServer(uowconn)
                    .Options;
                services.AddSingleton(uowOptions).AddTransient(typeof(FirstTestDBContext));
                //注入每个实体仓库
                var entities = from property in dbContextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                               where
                                   (ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbSet<>)) ||
                                    ReflectionHelper.IsAssignableToGenericType(property.PropertyType, typeof(DbQuery<>))) &&
                                   ReflectionHelper.IsAssignableToGenericType(property.PropertyType.GenericTypeArguments[0],
                                       typeof(IEntity<>))
                               select new EntityTypeInfo(property.PropertyType.GenericTypeArguments[0], property.DeclaringType);
                foreach (var entity in entities)
                {
                    var primaryKeyType = ReflectionHelper.GetPrimaryKeyType(entity.EntityType);
                    var protype = typeof(IRepository<>).MakeGenericType(entity.EntityType);
                    var eFprotype = typeof(EfCoreRepositoryBase<,>).MakeGenericType(entity.DeclaringType, entity.EntityType);
                    var protypekey = typeof(IRepository<,>).MakeGenericType(entity.EntityType, primaryKeyType);
                    var eFprotypekey = typeof(EfCoreRepositoryBase<,,>).MakeGenericType(entity.DeclaringType, entity.EntityType, primaryKeyType);
                    services.AddTransient<UnitOfWorkInterceptor, UnitOfWorkInterceptor>();
                    services.AddTransient(protype, eFprotype);
                    services.AddTransient(protypekey, eFprotypekey);
                }
            }
            services.BuildServiceProvider();
        }
    }
}
