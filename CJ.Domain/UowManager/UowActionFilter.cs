using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CJ.Domain.UowManager
{
    public class UowActionFilter: IAsyncActionFilter
    {
        //private readonly IUnitOfWorkDefaultOptions _unitOfWorkDefaultOptions;

        //public OnActionExecutionAsync(IUnitOfWorkDefaultOptions unitOfWorkDefaultOptions)
        //{
        //    _unitOfWorkDefaultOptions = unitOfWorkDefaultOptions;
        //}
        //public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        //{
        //    //if (!context.ActionDescriptor.IsControllerAction())
        //    //{
        //    //    await next();
        //    //    return;
        //    //}

        //var unitOfWorkAttr = _unitOfWorkDefaultOptions
        //                             .GetUnitOfWorkAttributeOrNull(context.ActionDescriptor.GetMethodInfo()) ??
        //                         _aspnetCoreConfiguration.DefaultUnitOfWorkAttribute;

        //    if (unitOfWorkAttr.IsDisabled)
        //    {
        //        await next();
        //        return;
        //    }

        //    using (var uow = _unitOfWorkManager.Begin(unitOfWorkAttr.CreateOptions()))
        //    {
        //        var result = await next();
        //        if (result.Exception == null || result.ExceptionHandled)
        //        {
        //            await uow.CompleteAsync();
        //        }
        //    }
        //}
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            throw new System.NotImplementedException();
        }
    }
}