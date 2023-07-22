using Artifactan.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Artifactan.Providers;

public class FilterExceptionProvider : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            BadHttpRequestException => new ObjectResult(new BaseResponse<object>(context.Exception.Message ?? "Bad Request", context.Exception))
            {
                StatusCode = 400
            },
            _ => new ObjectResult(new BaseResponse<object>(context.Exception.Message ?? "Internal Server Error", context.Exception))
            {
                StatusCode = 500
            },
        };
    }
}