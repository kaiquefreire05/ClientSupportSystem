using Microsoft.AspNetCore.Mvc.Filters;

namespace CustomerSupportSystem.Filters
{
    public class UserLoggedPage : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
