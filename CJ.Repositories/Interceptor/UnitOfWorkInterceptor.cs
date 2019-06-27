using System.Threading.Tasks;
using AspectCore.DynamicProxy;
using CJ.Domain.UowManager;
using CJ.Repositories.Extensions;

namespace CJ.Repositories.Interceptor
{
    public class UnitOfWorkInterceptor: AbstractInterceptor
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IUnitOfWorkDefaultOptions _unitOfWorkDefaultOptions;

        public UnitOfWorkInterceptor(IUnitOfWorkManager unitOfWorkManager, IUnitOfWorkDefaultOptions unitOfWorkOptions)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _unitOfWorkDefaultOptions = unitOfWorkOptions;
        }
        public override async Task Invoke(AspectContext context, AspectDelegate next)
        {
            var unitOfWorkAttr = _unitOfWorkDefaultOptions
                                    .GetUnitOfWorkAttributeOrNull(context.ImplementationMethod) ??  
                               new UnitOfWorkAttribute(); ;

            using (var uow = _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
            {
                await next(context);
                await uow.CompleteAsync();
            }
        }
    }
}