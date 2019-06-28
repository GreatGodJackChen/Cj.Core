using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace CJ.Core.Exception
{
    public class ApplicationErrorResult:ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}