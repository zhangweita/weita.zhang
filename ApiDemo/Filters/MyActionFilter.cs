﻿using Microsoft.AspNetCore.Mvc.Filters;
using static System.Net.Mime.MediaTypeNames;

namespace ApiDemo.Filters
{
    public class MyActionFilter1 : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("MyActionFilter 1:开始执行");
            ActionExecutedContext r = await next();
            if (r.Exception != null)
                Console.WriteLine("MyActionFilter 1:执行失败");
            else
                Console.WriteLine("MyActionFilter 1:执行成功");
        }
    }


    public class MyActionFilter2 : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            Console.WriteLine("MyActionFilter 2:开始执行");
            ActionExecutedContext r = await next();
            if (r.Exception != null)
                Console.WriteLine("MyActionFilter 2:执行失败");
            else
                Console.WriteLine("MyActionFilter 2:执行成功");
        }
    }
}
