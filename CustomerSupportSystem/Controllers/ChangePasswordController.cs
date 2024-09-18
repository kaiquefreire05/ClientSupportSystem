using CustomerSupportSystem.DTOs;
using CustomerSupportSystem.Helper.Interfaces;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportSystem.Controllers
{
    public class ChangePasswordController : Controller
    {
        // Injection Dependencies
        private readonly ISessionService _session;
        private readonly IUserRepository _userRepository;

        public ChangePasswordController(ISessionService session, IUserRepository userRepository)
        {
            _session = session;
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Change(ChangePassDto changePasswordDto)
        {
            try
            {
                var loggedUser = _session.GetUserSession();
                changePasswordDto.Id = loggedUser.Id;

                if (!ModelState.IsValid)
                {
                    return View("Index", changePasswordDto);
                }

                _userRepository.ChangePassword(changePasswordDto);
                TempData["SuccessMessage"] = "Password has been changed successfully.";
                return View("Index", changePasswordDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"We were unable to change your password. Error details: {ex.Message}";
                return View("Index", changePasswordDto);
            }
        }
    }
}