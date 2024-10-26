using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserManagement.Application.Interfaces;
using UserManagement.Application.Services;

namespace Presentation.CustomAttributes;

public class AccessControllAttribute : ActionFilterAttribute
{
    public string Perimission { get; set; }
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var userId = Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserGuid").Value);

        var _permissionService = context.HttpContext.RequestServices.GetService<IPerimissionServices>();

        if (!await _permissionService.CheckPerimission(userId, Perimission))
        {
            context.Result = new BadRequestObjectResult("Not Access");

        }
        else 
        {
            base.OnActionExecutionAsync(context, next);
        }
    }
}
