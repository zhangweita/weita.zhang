using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace ApiDemo.Filters;

/// <summary>
/// 非事务方法标识
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class NotTransactionalAttribute : Attribute
{

}

public class TransactionScopeFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        bool hasNotTransactionalAttribute = false;
        if (context.ActionDescriptor is ControllerActionDescriptor actionDesc)
        {
            hasNotTransactionalAttribute = actionDesc.MethodInfo.IsDefined(typeof(NotTransactionalAttribute));
        }
        if (hasNotTransactionalAttribute)
        {
            await next();
            return;
        }
        //异步代码中创建TransactionScope对象的时候，需要设定TransactionScopeAsyncFlowOption.Enabled这个构造方法的参数
        using TransactionScope txScope = new(TransactionScopeAsyncFlowOption.Enabled);
        var result = await next();
        if (result.Exception == null)//操作方法执行没有异常
        {
            txScope.Complete();
        }
    }
}
