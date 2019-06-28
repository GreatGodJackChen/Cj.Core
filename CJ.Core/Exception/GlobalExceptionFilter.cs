using Microsoft.AspNetCore.Mvc.Filters;
using System.Data.SqlClient;
using CJ.Core.Log4net;

namespace CJ.Core.Exception
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var errorMsg = "";
            if (context.Exception is SqlException)
            {
                errorMsg = @"系统繁忙，请稍后再试";
            }
            else
            {
                errorMsg = "未知错误,请重试";
                MyLogManager.Error(context.Exception.TargetSite.DeclaringType.ToString() + context.Exception.Message, context.Exception, this.GetType());
            }
            /*
             * 下面是我们自己处理、跳转到指定页面
             * context.Result=new RedirectResult("/home/Error");
             * 返回指定的错误对象
             *var json = new ErrorResponse("未知错误,请重试");
             */
            //public class ErrorResponse
            //{
            //    public ErrorResponse(string msg)
            //    {
            //        Message = msg;
            //    }
            //    public string Message { get; set; }
            //    public object DeveloperMessage { get; set; }
            //} 

            context.Result = new ApplicationErrorResult(errorMsg);
            context.ExceptionHandled = true;
        }
    }
}
