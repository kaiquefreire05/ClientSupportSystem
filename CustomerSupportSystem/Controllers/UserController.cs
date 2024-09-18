using CustomerSupportSystem.DTOs;
using CustomerSupportSystem.Filters;
using CustomerSupportSystem.Models;
using CustomerSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSupportSystem.Controllers
{
    [AdminRestrictedPage]
    public class UserController : Controller
    {
        // Dependencies Injection
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IActionResult Index()
        {
            var usuarios = _userRepository.GetAll();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Coverting UserModel to UserDTO
            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role
            };

            return View(userDto);
        }

        public IActionResult DeleteConfirm(int id)
        {
            var user = _userRepository.GetById(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult Create(UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = new UserModel
                    {
                        Name = userDto.Name,
                        Email = userDto.Email,
                        Password = userDto.Password,
                        Role = userDto.Role,
                        CreatedAt = DateTime.Now
                    };
                    _userRepository.Create(newUser);
                    TempData["SuccessMessage"] = "User created successfully";
                    return RedirectToAction("Index");
                }

                TempData["ErrorMessage"] = "Invalid data, Please check the form.";
                return View("Create", userDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"User was not registered. Try again. Error detail: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(UserNoPassDto userNoPassDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existentUser = userNoPassDto.Id.HasValue
                        ? _userRepository.GetById(userNoPassDto.Id.Value)
                        : null;
                    if (existentUser == null)
                    {
                        return NotFound("User not found");
                    }

                    existentUser.Name = userNoPassDto.Name;
                    existentUser.Email = userNoPassDto.Email;
                    existentUser.Role = userNoPassDto.Role;
                    existentUser.UpdatedAt = DateTime.Now;

                    _userRepository.Update(existentUser);

                    TempData["SuccessMessage"] = "User updated successfully.";
                    return RedirectToAction("Index");
                }

                return View(userNoPassDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error ocurred. Error details: {ex.Message}";
                return View(userNoPassDto);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var success = _userRepository.Delete(id);
                if (success)
                {
                    TempData["SuccessMessage"] = "User deleted successfuly.";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error. User was not deleted.";
                }

                return RedirectToAction("Index");
            }
            catch (SystemException ex)
            {
                TempData["ErrorMessage"] = $"Error. User was not deleted. Details: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}