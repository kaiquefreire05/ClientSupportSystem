using CustomerSupportSystem.Enums;
using CustomerSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CustomerSupportSystem.Filters
{
    public class AdminRestrictedPage : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var userSession = context.HttpContext.Session.GetString("loggedUserSession");

            if (string.IsNullOrEmpty(userSession))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                    { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                var user = JsonConvert.DeserializeObject<UserModel>(userSession);
                if (user == null)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Login" }, { "action", "Index" }
                        });
                }

                if (user.Role != RoleEnum.ADMIN)
                {
                    context.Result = new RedirectToRouteResult(
                        new RouteValueDictionary
                        {
                            { "controller", "Restrito" }, { "action", "Index" }
                        });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}