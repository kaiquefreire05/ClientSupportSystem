using ClientSupportSystem.DTOs;
using ClientSupportSystem.Helper.Interfaces;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientSupportSystem.Controllers
{
    public class LoginController : Controller
    {
        // Injecting Dependencie
        private readonly IUserRepository _userRep;
        private readonly ISessionService _session;
        public LoginController(IUserRepository userRep, ISessionService session)
        {
            _userRep = userRep;
            _session = session;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(LoginDto login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserModel user = _userRep.GetByEmail(login.Email);
                    if (user != null)
                    {
                        if (user.ValidPassword(login.Password))
                        {
                            _session.CreateUserSession(user);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["ErrorMessage"] = "User password is invalid. Please try again.";
                    }
                    TempData["ErrorMessage"] = "Invalid email or password. Please try again.";
                }
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Login error. Erro datails: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout()
        {
            _session.RemoveUserSession();
            return RedirectToAction("Index", "Login");
        }

    }
}
