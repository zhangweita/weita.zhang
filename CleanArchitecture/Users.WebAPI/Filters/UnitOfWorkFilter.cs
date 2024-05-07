using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Users.WebAPI.Filters;

public class UnitOfWorkFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        UnitOfWorkAttribute? uowAttr = GetUoWAttr(context.ActionDescriptor);
        if (uowAttr == null)
        {
            await next();
            return;
        }

        List<DbContext> dbContexts = [];
        foreach (var dbContextType in uowAttr.DbContextTypes)
        {
            var sp = context.HttpContext.RequestServices;
            DbContext dbContext = (DbContext)sp.GetRequiredService(dbContextType);
            dbContexts.Add(dbContext);
        }

        var result = await next();
        if (result.Exception == null) return;

        foreach (var dbContext in dbContexts)
        {
            await dbContext.SaveChangesAsync();
        }
    }

    private static UnitOfWorkAttribute? GetUoWAttr(ActionDescriptor actionDescriptor)
    {
        if (actionDescriptor is not ControllerActionDescriptor caDesc) return null;

        UnitOfWorkAttribute? uowAttr = caDesc.ControllerTypeInfo.GetCustomAttribute<UnitOfWorkAttribute>();
        return uowAttr ?? caDesc.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
    }
}
