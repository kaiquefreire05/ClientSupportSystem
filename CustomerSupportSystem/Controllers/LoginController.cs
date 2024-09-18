using CustomerSupportSystem.DTOs;
using CustomerSupportSystem.Helper.Interfaces;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportSystem.Controllers
{
    public class LoginController : Controller
    {
        private readonly IEmail _email;

        private readonly ISessionService _session;

        // Injecting Dependencies
        private readonly IUserRepository _userRep;

        public LoginController(IUserRepository userRep, ISessionService session, IEmail email)
        {
            _userRep = userRep;
            _session = session;
            _email = email;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Enter(LoginDto login)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                var user = _userRep.GetByEmail(login.Email);
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
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Login error. Error details: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult SendPasswordResetLink(ResetPasswordDto resetDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index");
                }

                var user = _userRep.GetByEmail(resetDto.Email);
                if (user != null)
                {
                    var newPass = user.GenerateNewPass();
                    var message = $"Your new password is: {newPass}";

                    var sent = _email.Sent(user.Email, "Customer Support System - New Password", message);
                    if (sent)
                    {
                        _userRep.Update(user);
                        TempData["SuccessMessage"] = "We have sent a new password to your registered email address.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "We were unable to send the email. Please try again.";
                    }

                    return RedirectToAction("Index", "Login");
                }

                TempData["ErrorMessage"] =
                    "We were unable to reset your password. Please check your entered information.";
                return View("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Oops, we were unable to reset your password, error details: {ex.Message}";
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