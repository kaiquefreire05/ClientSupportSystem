using ClientSupportSystem.DTOs;
using ClientSupportSystem.Filters;
using ClientSupportSystem.Models;
using ClientSupportSystem.Repositories;
using ClientSupportSystem.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientSupportSystem.Controllers
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
            IEnumerable<UserModel> usuarios = _userRepository.GetAll();
            return View(usuarios);
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null) return NotFound("User not found.");

            // Coverting UserModel to UserDTO
            UserDto userDto = new UserDto
            {
                Id = id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
            };

            return View(userDto);
        }

        [HttpPost]
        public IActionResult Create(UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newUser = new UserModel{
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
        public IActionResult Edit(UserDto userDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existentUser = userDto.Id.HasValue ? _userRepository.GetById(userDto.Id.Value) : null;
                    if (existentUser == null)
                    {
                        return NotFound("User not found");
                    }
                    existentUser.Name = userDto.Name;
                    existentUser.Email = userDto.Email;
                    existentUser.Role = userDto.Role;
                    existentUser.CreatedAt = DateTime.Now;

                    _userRepository.Update(existentUser);

                    TempData["SuccessMessage"] = "Ticket updated successfully.";
                    return RedirectToAction("Index");
                }
                return View(userDto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error ocurred. Error details: {ex.Message}";
                return View(userDto);
            }
        }
    }
}
