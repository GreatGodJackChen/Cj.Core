using System;
using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using CJ.Domain.UowManager;
using CJ.Repositories.Extensions;

namespace CJ.Repositories.Interceptor
{
    public class UnitOfWorkAttribute1: AbstractInterceptorAttribute
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUnitOfWorkDefaultOptions _unitOfWorkDefaultOptions;

        public UnitOfWorkAttribute1(IUnitOfWorkManager unitOfWorkManager, IUnitOfWorkDefaultOptions unitOfWorkOptions)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _unitOfWorkDefaultOptions = unitOfWorkOptions;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
             var unitOfWorkAttr = _unitOfWorkDefaultOptions
                                     .GetUnitOfWorkAttributeOrNull(context.ActionDescriptor.GetMethodInfo()) ??
                                 _aspnetCoreConfiguration.DefaultUnitOfWorkAttribute;

            using (var uow = _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
            {
                var result = await next(context);
                if (result.Exception == null || result.ExceptionHandled)
                {
                    await uow.CompleteAsync();
                }
            }
            try
            {
                Console.WriteLine("Before service call");
               
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}