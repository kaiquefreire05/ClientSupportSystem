using Microsoft.AspNetCore.Mvc.Filters;

namespace ClientSupportSystem.Filters
{
    public class UserLoggedPage : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
