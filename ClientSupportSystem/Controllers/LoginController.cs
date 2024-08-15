using Microsoft.AspNetCore.Mvc;

namespace ClientSupportSystem.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
