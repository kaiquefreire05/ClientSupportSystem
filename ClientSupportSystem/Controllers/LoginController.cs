using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientSupportSystem.Controllers
{
    public class LoginController : Controller
    {
        // Injecting Dependencie
        private readonly IUserRepository _userRep;
        public LoginController(IUserRepository _userRep)
        {
            _userRep = _userRep;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(LoginModel login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRep.GetByEmail(login.Email);
                    if (user != null)
                    {
                        
                    }
                }
            }
        }
    }
}
