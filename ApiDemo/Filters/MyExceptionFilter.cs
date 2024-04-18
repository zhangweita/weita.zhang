using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiDemo.Filters;

public class MyExceptionFilter(ILogger<MyExceptionFilter> _logger, IHostEnvironment _hostEnvironment) : IAsyncExceptionFilter
{
    private readonly ILogger<MyExceptionFilter> _logger = _logger;
    private readonly IHostEnvironment _hostEnvironment = _hostEnvironment;

    public Task OnExceptionAsync(ExceptionContext context)
    {
        Exception exception = context.Exception;
        _logger.LogError(exception, "UnhandledException occured");

        string message = _hostEnvironment.IsDevelopment() ? $"{exception}" : "程序中出现未处理异常";

        ObjectResult result = new(new { code = 500, message }) { StatusCode = 500 };
        context.Result = result;
        context.ExceptionHandled = true;
        return Task.CompletedTask;
    }
}
