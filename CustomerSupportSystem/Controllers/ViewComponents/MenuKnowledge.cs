using CustomerSupportSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerSupportSystem.Controllers.ViewComponents
{
    public class MenuKnowledge : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userSession = HttpContext.Session.GetString("loggedUserSession");
            if (string.IsNullOrEmpty(userSession))
            {
                return null;
            }

            var user = JsonConvert.DeserializeObject<UserModel>(userSession);
            return View("DefaultKnowledge", user);
        }
    }
}