using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportSystem.Controllers
{
    public class KnowledgeController : Controller
    {
        // Dependencies Injection


        public IActionResult Index()
        {
            return View();
        }
    }
}
