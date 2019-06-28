using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using CJ.Domain.UowManager;
using CJ.Repositories.Extensions;

namespace CJ.Repositories.Interceptor
{
    public class UnitOfWorkInterceptor: AbstractInterceptor
    {
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var unitOfWorkManager = (UnitOfWorkManager)context.ServiceProvider.GetService(typeof(IUnitOfWorkManager));
            var unitOfWorkDefaultOptions = (UnitOfWorkDefaultOptions)context.ServiceProvider.GetService(typeof(IUnitOfWorkDefaultOptions));
            var unitOfWorkAttr = unitOfWorkDefaultOptions
                                    .GetUnitOfWorkAttributeOrNull(context.ImplementationMethod) ??
                               new UnitOfWorkAttribute(); ;

            using (var uow = unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
            {
                try
                {
                    await next(context);
                    await uow.CompleteAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}